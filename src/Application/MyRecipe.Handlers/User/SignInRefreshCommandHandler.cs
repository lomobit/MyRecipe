using MediatR;
using MyRecipe.AppServices.User;
using MyRecipe.Contracts.User;
using MyRecipe.Handlers.Contracts.User;

namespace MyRecipe.Handlers.User;

public class SignInRefreshCommandHandler : IRequestHandler<SignInRefreshCommand, TokenDto?>
{
    private readonly IUserService _userService;

    public SignInRefreshCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<TokenDto?> Handle(SignInRefreshCommand request, CancellationToken cancellationToken)
    {
        return await _userService.GetUserTokenWithRefreshTokenAsync(request.RefreshToken, cancellationToken);
    }
}