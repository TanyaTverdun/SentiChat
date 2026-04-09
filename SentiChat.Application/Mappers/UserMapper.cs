using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Chats;
using SentiChat.Application.DTOs.Users;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Mappers;

/// <summary>
/// Provides high-performance mapping methods 
/// for User-related entities using Riok.Mapperly.
/// </summary>
[Mapper]
public static partial class UserMapper
{
    /// <summary>
    /// Maps a <see cref="RegisterUserDto"/> to a new <see cref="User"/> entity.
    /// Explicitly ignores navigation properties and real-time status fields.
    /// </summary>
    /// <param name="dto">
    /// The registration data provided by the user.
    /// </param>
    /// <param name="id">
    /// The generated unique identifier for the new user.
    /// </param>
    /// <param name="passwordHash">
    /// The securely hashed password.
    /// </param>
    /// <returns>
    /// A new <see cref="User"/> entity ready to be saved to the database.
    /// </returns>
    [MapperIgnoreTarget(nameof(User.Bio))]
    [MapperIgnoreTarget(nameof(User.SentMessages))]
    [MapperIgnoreTarget(nameof(User.ChatMembers))]
    [MapperIgnoreSource(nameof(RegisterUserDto.Password))]
    [MapperIgnoreTarget(nameof(User.IsOnline))]
    [MapperIgnoreTarget(nameof(User.LastSeen))]
    public static partial User ToEntity(
        this RegisterUserDto dto, 
        Guid id, 
        string passwordHash);

    /// <summary>
    /// Wraps a generated JWT token string into 
    /// a standard authentication response DTO.
    /// </summary>
    /// <param name="token">
    /// The JWT token string.
    /// </param>
    /// <returns>
    /// An <see cref="AuthResponseDto"/> containing the token.
    /// </returns>
    public static AuthResponseDto MapToAuthResponseDto(this string token)
    {
        return new AuthResponseDto
        {
            Token = token
        };
    }

    /// <summary>
    /// Maps a <see cref="User"/> entity to a <see cref="UserProfileDto"/>, 
    /// using the explicitly provided initials. 
    /// Unnecessary internal source properties 
    /// (such as relationships and security fields) are explicitly ignored.
    /// </summary>
    /// <param name="user">
    /// The source user entity containing the profile data.
    /// </param>
    /// <param name="initials">
    /// The dynamically calculated initials to assign to the DTO.
    /// </param>
    /// <returns>
    /// A new instance of <see cref="UserProfileDto"/> 
    /// populated with the user's public data.
    /// </returns>
    [MapperIgnoreSource(nameof(User.IsOnline))]
    [MapperIgnoreSource(nameof(User.PasswordHash))]
    [MapperIgnoreSource(nameof(User.ChatMembers))]
    [MapperIgnoreSource(nameof(User.LastSeen))]
    [MapperIgnoreSource(nameof(User.SentMessages))]
    public static partial UserProfileDto ToProfileDto(this User user, string initials);

    /// <summary>
    /// Updates an existing <see cref="User"/> entity in-place using data from an 
    /// <see cref="UpdateProfileRequestDto"/>. System-managed and sensitive fields 
    /// (like Id, PasswordHash, and tracking properties) are explicitly ignored.
    /// </summary>
    /// <param name="dto">
    /// The data transfer object containing the new profile information.
    /// </param>
    /// <param name="user">
    /// The existing user entity that will receive the updated values.
    /// </param>
    [MapperIgnoreTarget(nameof(User.IsOnline))]
    [MapperIgnoreTarget(nameof(User.PasswordHash))]
    [MapperIgnoreTarget(nameof(User.ChatMembers))]
    [MapperIgnoreTarget(nameof(User.LastSeen))]
    [MapperIgnoreTarget(nameof(User.SentMessages))]
    [MapperIgnoreTarget(nameof(User.Id))]
    public static partial void UpdateEntity(this UpdateProfileRequestDto dto, User user);
}
