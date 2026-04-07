using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SentiChat.Infrastructure.Security;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var secretKey = this._configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT Secret key is missing in configuration.");
        }

        var issuer = this._configuration["Jwt:Issuer"];
        var audience = this._configuration["Jwt:Audience"];
        var expirationMinutes = int.Parse(this._configuration["Jwt:ExpirationMinutes"] ?? "60");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
