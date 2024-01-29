using System.Security.Claims;

namespace MyRecipe.Api.Managers.User;

public static class UserExtensionManager
{
    public static Guid? GetId(this ClaimsPrincipal user)
    {
        var tempResult = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (tempResult is null)
        {
            return null;
        }

        if (!Guid.TryParse(tempResult, out var result))
        {
            return null;
        }

        return result;
    }
}