using MediatR;

namespace MyRecipe.Handlers.Contracts.User;

public record SignInCommand(string Email, string Password) : IRequest<string?>;