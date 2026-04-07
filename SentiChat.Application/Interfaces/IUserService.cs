using SentiChat.Application.DTOs.Users;

namespace SentiChat.Application.Interfaces;

/// <summary>
/// Defines the contract for user-related business operations.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="registerUser">The data transfer object containing user registration details.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The unique identifier (GUID) of the newly registered user.</returns>
    /// <exception cref="ArgumentException">Thrown when a user with the provided email already exists.</exception>
    public Task<Guid> RegisterUserAsync(RegisterUserDto registerUser, CancellationToken cancellationToken);
}
