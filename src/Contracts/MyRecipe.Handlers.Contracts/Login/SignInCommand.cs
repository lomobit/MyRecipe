using MediatR;

namespace MyRecipe.Handlers.Contracts.Login;

public record SignInCommand(string Email, string Password) : IRequest<string>;