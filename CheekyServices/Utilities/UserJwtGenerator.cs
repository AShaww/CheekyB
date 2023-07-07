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
    public bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.ValidTo <= DateTime.UtcNow;
    }
    public string GenerateToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, $"{user.FirstName} {user.Surname}"),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "User")
        };

        var token = new JwtSecurityToken
        (
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
                SecurityAlgorithms.HmacSha256)
        );
        token.Payload.Add("sub", user.UserId);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}