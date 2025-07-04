using System.Security.Cryptography;
using ServiceScheduling.Application.Interfaces;

namespace ServiceScheduling.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 128/8;
    private const int KeySize = 256/8;
    private const int Integrations = 10000;
    private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
    private static readonly char Delimiter = ';';
    
    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Integrations, _hashAlgorithm, KeySize);

        return string.Join(Delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        var elements = passwordHash.Split(Delimiter);
        var salt = Convert.FromBase64String(elements[0]);
        var hash = Convert.FromBase64String(elements[1]);

        var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Integrations, _hashAlgorithm, KeySize);
        
        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}