using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyRecipe.Api.Managers.User;
using MyRecipe.Contracts.Api;
using MyRecipe.Contracts.Enums.User;
using MyRecipe.Contracts.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Api.Controllers.v1;

[Route("api/v1/[controller]")]
[ProducesResponseType(typeof(string), statusCode: StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(string), statusCode: StatusCodes.Status500InternalServerError)]
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signin")]
    [ProducesResponseType(typeof(TokenDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result is null)
            {
                return Unauthorized();
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
#if DEBUG
            return BadRequest(ex.ToString());
#else
            return Unauthorized();
#endif
        }
    }

    [HttpPost("signin/refresh")]
    [ProducesResponseType(typeof(TokenDto), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignInWithRefreshToken([FromBody] SignInRefreshCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result is null)
            {
                return Unauthorized();
            }
            
            return Ok(result);
        }
        catch (Exception ex)
        {
#if DEBUG
            return BadRequest(ex.ToString());
#else
            return Unauthorized();
#endif
        }
    }
    
    [HttpPost("signup")]
    [ProducesResponseType(typeof(bool), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result ? "Success" : "Failure");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("editprofile")]
    [ProducesResponseType(typeof(ApiResult<bool>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status401Unauthorized)]
    [Authorize(Roles = $"{nameof(RoleEnum.Visitor)}")]
    public async Task<IActionResult> EditVisitorProfile(
        [FromBody] EditVisitorProfileWithoutUserIdCommand command,
        CancellationToken cancellationToken)
    {
        var userId = User.GetId();
        if (userId is null)
        {
            return BadRequest($"Ошибка: Невозможно определить пользователя. Попробуйте выйти из системы и заново войти.");
        }
        
        var newCommand = new EditVisitorProfileCommand(
            userId.Value,
            command.FirstName,
            command.MiddleName,
            command.LastName,
            command.Email
            );

        return await CallApiActionWithResultAsync(async () => await _mediator.Send(newCommand, cancellationToken));
    }
}