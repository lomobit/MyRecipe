using MediatR;

namespace MyRecipe.Handlers.Contracts.User;

public record SignUpCommand(string Email, string Password) : IRequest<bool>;