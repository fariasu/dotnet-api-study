using TaskManager.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace TaskManager.Infrastructure.Security.Cryptography;

public class PasswordEncrypter : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        var passwordHash = BC.HashPassword(password);

        return passwordHash;
    }

    public bool Verify(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}