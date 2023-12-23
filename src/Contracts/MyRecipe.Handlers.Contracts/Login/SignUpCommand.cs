using MediatR;

namespace MyRecipe.Handlers.Contracts.Login;

public record SignUpCommand(string Email, string Password) : IRequest<bool>;