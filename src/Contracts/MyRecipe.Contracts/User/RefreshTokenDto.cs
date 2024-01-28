namespace MyRecipe.Contracts.User;

public record RefreshTokenDto(Guid UserId, string Token, DateTime ExpirationTime);