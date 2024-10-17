using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
        => _options = options.Value;

    public string GenerateToken(User user)
    {
        Claim[] claims = [new Claim( ClaimTypes.NameIdentifier, user.Id.ToString())];
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials:signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
        );
        
        var tockenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tockenValue;
    }
}

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpiresHours { get; set; }
}