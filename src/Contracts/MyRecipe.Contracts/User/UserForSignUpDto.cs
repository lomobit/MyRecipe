namespace MyRecipe.Contracts.User;

public record UserForSignUpDto(
    string Email,
    string FirstName,
    string MiddleName,
    string LastName,
    string PasswordHash,
    string PasswordHashSalt
    );