using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Services;
using TaskManager.Infrastructure.DataAccess.Db;

namespace TaskManager.Infrastructure.Services;

public class LoggedUserService : ILoggedUserService
{
    private readonly TaskManagerDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;

    public LoggedUserService(TaskManagerDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }

    public async Task<UserEntity> GetUserAsync()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);
        
        var user = await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.UserIdentifier == userIdentifier);
        return user;

    }

}