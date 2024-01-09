using MediatR;
using MyRecipe.Contracts.User;

namespace MyRecipe.Handlers.Contracts.User;

public record SignInCommand(string Email, string Password) : IRequest<TokenDto?>;