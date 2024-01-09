using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
    public async Task<TokenDto?> GetUserTokenAsync(string email, string password)
    {
        // Находим пользователя по почте
        var userPassword = await _userRepository.GetUserIdWithPassword(email);
        if (userPassword is null)
        {
            return null;
        }

        // Проверяем корректность пароля.
        if (!IsPasswordCorrect(
                password,
                userPassword.PasswordHash, 
                Convert.FromHexString(userPassword.PasswordHashSalt)))
        {
            return null;
        }

        // Получаем данные пользователя для того, чтобы задать список claim'ов для токена
        var user = await _userRepository.GetUserForSignInAsync(userPassword.UserId);
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, string.Join(" ",  user.LastName, user.FirstName, user.MiddleName)),
            new(ClaimTypes.Role, user.Role.ToString()),
        };

        // Создаем JWT-токен
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
    
    private bool IsPasswordCorrect(string password, string hash, byte[] salt)
    {
        if (!int.TryParse(_configuration["Login:PasswordHashKeySize"], out int keySize))
        {
            // TODO: Добавить логгирование ошибки
            throw new ApplicationException($"Configuration \"Login:PasswordHashKeySize\" doesn't exists");
        }
        
        if (!int.TryParse(_configuration["Login:PasswordHashIterations"], out int iterations))
        {
            // TODO: Добавить логгирование ошибки
            throw new ApplicationException($"Configuration \"Login:PasswordHashIterations\" doesn't exists");
        }
        
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}