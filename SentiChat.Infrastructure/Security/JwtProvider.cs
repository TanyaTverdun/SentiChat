using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Domain.Entities;
using SentiChat.Infrastructure.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SentiChat.Infrastructure.Security;

/// <summary>
/// Provides the standard implementation of <see cref="IJwtProvider"/>
/// </summary>
public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        this._options = options.Value;
    }

    /// <inheritdoc />
    public string GenerateToken(User user)
    {
        if (string.IsNullOrEmpty(this._options.SecretKey))
        {
            throw new InvalidOperationException(
                "JWT Secret key is missing in configuration.");
        }

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this._options.SecretKey));
        var credentials = new SigningCredentials(
            securityKey, 
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: this._options.Issuer,
            audience: this._options.Audience,
            claims: claims,
            expires: DateTime.UtcNow
                .AddMinutes(this._options.ExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
