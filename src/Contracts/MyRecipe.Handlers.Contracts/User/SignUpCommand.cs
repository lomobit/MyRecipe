using MediatR;

namespace MyRecipe.Handlers.Contracts.User;

public record SignUpCommand(
    string Email,
    string Password,
    string FirstName,
    string MiddleName,
    string LastName) : IRequest<bool>;