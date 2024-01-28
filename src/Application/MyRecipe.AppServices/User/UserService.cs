using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyRecipe.Contracts.User;
using MyRecipe.Handlers.Contracts.User;
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
    public async Task<TokenDto?> GetUserTokenAsync(string email, string password, CancellationToken cancellationToken)
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

        return await CreateNewTokenAsync(userPassword.UserId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TokenDto?> GetUserTokenWithRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken)
    {
        // Находим рефреш-токен
        var refreshTokenInfo = await _userRepository.TryToFindRefreshTokenAsync(refreshToken, cancellationToken);
        if (refreshTokenInfo is null)
        {
            return null;
        }
        
        // Проверяем актуален ли этот рефреш-токен
        if (DateTime.UtcNow >= refreshTokenInfo.ExpirationTime)
        {
            return null;
        }
        
        return await CreateNewTokenAsync(refreshTokenInfo.UserId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> UserRegistration(SignUpCommand command, CancellationToken cancellationToken)
    {
        var hash = HashPasword(command.Password, out var salt);
        
        var newUser = new UserForSignUpDto(
            command.Email,
            command.FirstName,
            command.MiddleName,
            command.LastName,
            hash,
            salt);

        return await _userRepository.RegisterNewUser(newUser, cancellationToken);
    }

    private async Task<TokenDto?> CreateNewTokenAsync(Guid userId, CancellationToken cancellationToken)
    {
        // Получаем данные пользователя для того, чтобы задать список claim'ов для токена
        var user = await _userRepository.GetUserForSignInAsync(userId, cancellationToken);
        if (user is null)
        {
            return null;
        }
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, string.Join(" ",  user.LastName, user.FirstName, user.MiddleName)),
            new(ClaimTypes.Role, user.Role.ToString()),
            new (ClaimTypes.NameIdentifier, userId.ToString())
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
            userId,
            GenerateRefreshToken(),
            DateTime.UtcNow.Add(JsonConvert.DeserializeObject<TimeSpan>(_configuration["JwtSettings:RefreshTokenExpired"]!)));

        await _userRepository.AddUserRefreshTokenAsync(refreshToken, cancellationToken);
        
        return new TokenDto(token, refreshToken.Token);
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private (int keySize, int iterations, HashAlgorithmName hashAlgorithm) CheckIfExistsAndGetPasswordHashValues()
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
        
        return (keySize, iterations, HashAlgorithmName.SHA512);
    }
    
    private string HashPasword(string password, out string salt)
    {
        var (keySize, iterations, hashAlgorithm) = CheckIfExistsAndGetPasswordHashValues();
        
        var bytesForSalt = RandomNumberGenerator.GetBytes(keySize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            bytesForSalt,
            iterations,
            hashAlgorithm,
            keySize);

        salt = Convert.ToHexString(bytesForSalt);
        return Convert.ToHexString(hash);
    }
    
    private bool IsPasswordCorrect(string password, string hash, byte[] salt)
    {
        var (keySize, iterations, hashAlgorithm) = CheckIfExistsAndGetPasswordHashValues();
        
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
        return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
    }
}