using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookMyMovie.Application.Common.Authentication;
using BookMyMovie.Application.Common.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BookMyMovie.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider,IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }


    public string GenerateJwtToken(Guid userId, string role, string userName, string FirstName)
    {
        var siginingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.Secret)
            ),
            SecurityAlgorithms.HmacSha256
        );
        System.Console.WriteLine(userId);
        System.Console.WriteLine(userId);
        var claims = new[]
        {
            
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, role),   
            new Claim(JwtRegisteredClaimNames.GivenName,userId.ToString()),
            new Claim(JwtRegisteredClaimNames.FamilyName, FirstName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };
        var securityToken = new JwtSecurityToken(
            issuer:_jwtSettings.Issuer,
            expires: _dateTimeProvider.UtcNow.AddDays(333333),
            claims: claims, 
            audience:_jwtSettings.Audience,
            signingCredentials:siginingCredentials);
        
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}