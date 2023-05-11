using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CheekyModels.Dtos;
using CheekyServices.Configuration;
using CheekyServices.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace CheekyServices.Utilities;

public class UserJwtGenerator : IUserJwtGenerator
{
    private readonly JWTConfiguration _jwtConfig;

    public UserJwtGenerator(JWTConfiguration jwtConfig)
    {
        _jwtConfig = jwtConfig;
    }

    public string GenerateToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken
        (
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
                SecurityAlgorithms.HmacSha256)
        );
        token.Payload.Add("sub", user.UserId);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}