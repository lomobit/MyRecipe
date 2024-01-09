namespace MyRecipe.Contracts.User;

public record TokenDto(
    string Token,
    string RefreshToken
);