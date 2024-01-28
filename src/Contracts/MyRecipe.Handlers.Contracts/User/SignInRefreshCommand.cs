using MediatR;
using MyRecipe.Contracts.User;

namespace MyRecipe.Handlers.Contracts.User;

public record SignInRefreshCommand(string RefreshToken) : IRequest<TokenDto?>;