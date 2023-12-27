using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.Contracts.User;
using MyRecipe.Infrastructure.Repositories.User;

namespace MyRecipe.AppServices.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public UserService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <inheritdoc/>
    public async Task<string?> GetUserTokenAsync(string email, string password)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, email)
        };
        
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(25)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)), 
                SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
        
        // var claims = new Claim[]
        // {
        //
        // };
        //
        // var signingCredentials = new SigningCredentials(
        //     new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret-key")),
        //     SecurityAlgorithms.HmacSha512);
        //
        // var token = new JwtSecurityToken(
        //     "issuer",
        //     "audience",
        //     claims,
        //     null,
        //     DateTime.Now.AddMinutes(25),
        //     signingCredentials);
        //
        // var tokenValue = new JwtSecurityTokenHandler()
        //     .WriteToken(token);
        //
        // return tokenValue;
    }
}