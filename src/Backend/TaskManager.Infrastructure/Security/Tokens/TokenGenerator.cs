using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Security.Tokens;

namespace TaskManager.Infrastructure.Security.Tokens;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserEntity userEntity)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _configuration.GetValue<string>("Settings:Jwt:SecretKey");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.Name, userEntity.Name),
                new Claim(ClaimTypes.Sid, userEntity.UserIdentifier.ToString())
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(GetSymmetricSecurityKey(key!), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        return new SymmetricSecurityKey(keyBytes);
    }
}