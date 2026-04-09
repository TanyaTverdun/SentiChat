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
    /// <param name="registerUser">
    /// The data transfer object containing user registration details.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The JWT token as a string.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown when a user with the provided email already exists.
    /// </exception>
    public Task<string> RegisterUserAsync(
        RegisterUserDto registerUser, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Authenticates a user based on the provided credentials.
    /// </summary>
    /// <param name="loginUserDto">
    /// The data transfer object containing login credentials 
    /// (email and password).
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The JWT token as a string.
    /// </returns>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the email is not found or the password does not match.
    /// </exception>
    public Task<string> LoginUserAsync(
        LoginUserDto loginUserDto, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the profile information for a specific user.
    /// </summary>
    /// <param name="userId">
    /// The unique identifier (GUID) of the user 
    /// whose profile is being requested.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>A <see cref="UserProfileDto"/> 
    /// containing the user's profile details.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when a user with the specified ID cannot be found.
    /// </exception>
    Task<UserProfileDto> GetUserProfileAsync(
        Guid userId, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Updates the profile information for a specific user.
    /// </summary>
    /// <param name="userId">
    /// The unique identifier (GUID) of the user being updated.
    /// </param>
    /// <param name="request">
    /// The data transfer object containing the new profile information.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>A <see cref="UserProfileDto"/>
    /// containing the updated user profile details.
    /// </returns>
    /// <exception cref="KeyNotFoundException">
    /// Thrown when a user with the specified ID cannot be found.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the requested new email address 
    /// is already in use by another account.
    /// </exception>
    Task<UserProfileDto> UpdateUserProfileAsync(
        Guid userId, 
        UpdateProfileRequestDto request, 
        CancellationToken cancellationToken);
}
