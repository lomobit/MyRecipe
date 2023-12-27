﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.Contracts.Enums.User;
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
            new(ClaimTypes.Name, string.Join(" ", user.FirstName, user.MiddleName, user.LastName)),
            new(ClaimTypes.Email, email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        
        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.Add(TimeSpan.FromMinutes(25)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!)), 
                SecurityAlgorithms.HmacSha256));
            
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}