using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.Contracts.User;
using MyRecipe.Infrastructure.Repositories.User;

namespace MyRecipe.AppServices.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public async Task<string?> GetUserTokenAsync()
    {
        var claims = new Claim[]
        {

        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key")),
            SecurityAlgorithms.HmacSha512);
        
        var token = new JwtSecurityToken(
            "issuer",
            "audience",
            claims,
            null,
            DateTime.Now.AddMinutes(25),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);
        
        return tokenValue;
    }
}