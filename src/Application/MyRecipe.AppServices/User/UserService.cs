﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.Contracts.Enums.User;
using MyRecipe.Contracts.User;
using MyRecipe.Infrastructure.Repositories.User;
using Newtonsoft.Json;

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

    /// <inheritdoc/>
    public async Task<TokenDto> GetUserTokenAsync(string email, string password)
    {
        // находим пользователя по почте и паролю из репозитория
        var user = new UserForSignInDto(
            "Алексей",
            "Михайлович",
            "Кузнецов",
            "test@test.net",
            RoleEnum.Administrator
        );
        
        // если пользователь не найден, отправляем статусный код 401
        //if (user is null) return null;
        if (user.Email != email)
        {
            return null;
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, string.Join(" ",  user.LastName, user.FirstName, user.MiddleName)),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(JsonConvert.DeserializeObject<TimeSpan>(_configuration["JwtSettings:TokenExpired"]!)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)), 
                SecurityAlgorithms.HmacSha256));
            
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        
        var refreshToken = new RefreshTokenDto(
            GenerateRefreshToken(),
            DateTime.UtcNow.Add(JsonConvert.DeserializeObject<TimeSpan>(_configuration["JwtSettings:RefreshTokenExpired"]!)));

        
        return new TokenDto(token, refreshToken.Token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}