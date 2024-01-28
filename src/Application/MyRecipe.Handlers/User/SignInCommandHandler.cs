using MediatR;
using MyRecipe.AppServices.User;
using MyRecipe.Contracts.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Handlers.User;

public class SignInCommandHandler : IRequestHandler<SignInCommand, TokenDto?>
{
    private readonly IUserService _userService;

    public SignInCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<TokenDto?> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserTokenAsync(request.Email, request.Password, cancellationToken);
    }
}