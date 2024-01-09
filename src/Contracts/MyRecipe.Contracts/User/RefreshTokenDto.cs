namespace MyRecipe.Contracts.User;

public record RefreshTokenDto(string Token, DateTime ExpirationTime);