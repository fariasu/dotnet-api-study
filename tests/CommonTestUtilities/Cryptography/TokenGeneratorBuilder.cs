using Moq;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Security.Tokens;

namespace CommonTestUtilities.Cryptography;

public class TokenGeneratorBuilder
{
    public static ITokenGenerator Build()
    {
        var mock = new Mock<ITokenGenerator>();

        mock.Setup(tokenGenerator => tokenGenerator.GenerateToken(It.IsAny<UserEntity>())).Returns("token");
        
        return mock.Object;
    }
}