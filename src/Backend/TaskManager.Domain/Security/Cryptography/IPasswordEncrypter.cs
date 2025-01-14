namespace TaskManager.Domain.Security.Cryptography;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    public bool Verify(string password, string encryptedPassword);
}