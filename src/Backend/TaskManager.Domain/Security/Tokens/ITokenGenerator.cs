using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Security.Tokens;

public interface ITokenGenerator
{
    public string GenerateToken(UserEntity user);
}