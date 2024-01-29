using MediatR;

namespace MyRecipe.Handlers.Contracts.User;

public record EditVisitorProfileWithoutUserIdCommand(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email
    );

public record EditVisitorProfileCommand(
    Guid UserId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email
) : IRequest<bool>;