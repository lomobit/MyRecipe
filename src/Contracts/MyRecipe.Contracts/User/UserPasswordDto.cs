namespace MyRecipe.Contracts.User;

public record UserPasswordDto(
    Guid UserId,
    string PasswordHash,
    string PasswordHashSalt
);