﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    [ProducesResponseType(typeof(string), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
#if DEBUG
            return BadRequest(ex.ToString());
#endif
        }
    }
    
    [HttpPost("signup")]
    [ProducesResponseType(typeof(bool), statusCode: StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        return Ok();
    }
}