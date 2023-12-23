namespace MyRecipe.Domain.User;

/// <summary>
/// Сущность пользователя 
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Актуальная версия состояния пользователя.
    /// </summary>
    public uint Version { get; set; }

    /// <summary>
    /// Дата создания учетной записи.
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// Состояния учетной записи пользователя.
    /// </summary>
    public virtual List<UserState> States { get; set; }


    /*
     Example from: https://code-maze.com/csharp-hashing-salting-passwords-best-practices/

     const int keySize = 64;
     const int iterations = 350000;
     HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

     string HashPasword(string password, out byte[] salt)
     {
         salt = RandomNumberGenerator.GetBytes(keySize);
         var hash = Rfc2898DeriveBytes.Pbkdf2(
             Encoding.UTF8.GetBytes(password),
             salt,
             iterations,
             hashAlgorithm,
             keySize);
          return Convert.ToHexString(hash);
        }

        var hash = HashPasword("clear_password", out var salt);
        Console.WriteLine($"Password hash: {hash}");
        Console.WriteLine($"Generated salt: {Convert.ToHexString(salt)}");

        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);
            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }




     */
}