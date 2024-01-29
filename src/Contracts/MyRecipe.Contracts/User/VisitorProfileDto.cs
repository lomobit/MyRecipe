namespace MyRecipe.Contracts.User;

public record VisitorProfileDto(
    Guid UserId,
    string FirstName,
    string MiddleName,
    string LastName,
    string Email
    );