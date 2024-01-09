using MyRecipe.Contracts.Enums.User;

namespace MyRecipe.Contracts.User;

public record UserForSignInDto(
    string FirstName,
    string MiddleName,
    string LastName,
    RoleEnum Role
);