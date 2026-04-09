using SentiChat.Application.Interfaces.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SentiChat.Security;

/// <summary>
/// Provides access to the current authenticated 
/// user's information from the HTTP context.
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Retrieves the unique identifier of the currently authenticated user.
    /// </summary>
    /// <returns>
    /// The user's unique identifier (GUID).
    /// </returns>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the user is not authenticated 
    /// or the ID claim is missing/invalid.
    /// </exception>
    public Guid GetUserId()
    {
        var user = this._httpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                       ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (Guid.TryParse(userIdClaim, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException(
            "User is not authenticated or ID claim is missing.");
    }
}
