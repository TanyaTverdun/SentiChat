namespace SentiChat.Infrastructure.Configuration;

/// <summary>
/// Represents the configuration options for JSON Web Token (JWT) authentication.
/// These values are typically bound from the appsettings.json file.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// The name of the configuration section where these settings are located.
    /// </summary>
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// The secret key used to symmetrically sign the JWT.
    /// </summary>
    public string SecretKey { get; init; } = string.Empty;

    /// <summary>
    /// The issuer claim, representing the entity that generated the token.
    /// </summary>
    public string Issuer { get; init; } = string.Empty;

    /// <summary>
    /// The audience claim, representing the intended recipient of the token.
    /// </summary>
    public string Audience { get; init; } = string.Empty;

    /// <summary>
    /// The lifespan of the generated token in minutes. Defaults to 60 minutes.
    /// </summary>
    public int ExpiryMinutes { get; init; } = 60;
}