using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace ToDoApi.Interfaces;

public interface IAuthService
{
    Task<JwtSecurityToken> CreateTokenAsync(IdentityUser user);
}