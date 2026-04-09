using SentiChat.Application.DTOs.Users;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Application.Mappers;
using SentiChat.Domain.Interfaces.Repositories;

namespace SentiChat.Application.Services;

/// <summary>
/// Provides the standard implementation of <see cref="IUserService"/>.
/// </summary>
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        this._unitOfWork = unitOfWork;
        this._userRepository = userRepository;
        this._passwordHasher = passwordHasher;
        this._jwtProvider = jwtProvider;
    }

    /// <inheritdoc />
    public async Task<string> RegisterUserAsync(
        RegisterUserDto registerUser,
        CancellationToken cancellationToken)
    {
        var userEmailExists = await this._userRepository
            .ExistsByEmailAsync(
                registerUser.Email,
                cancellationToken);

        if (userEmailExists)
        {
            throw new ArgumentException(
                $"User with email {registerUser.Email} already exists.");
        }

        string passwordHash = this._passwordHasher
            .HashPassword(registerUser.Password);

        var  userId = Guid.NewGuid();

        var newUser = registerUser.ToEntity(
            userId,
            passwordHash);

        await this._userRepository.AddAsync(
            newUser, 
            cancellationToken);

        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        var token = this._jwtProvider
            .GenerateToken(newUser);

        return token;
    }

    /// <inheritdoc />
    public async Task<string> LoginUserAsync(
        LoginUserDto loginUserDto, 
        CancellationToken cancellationToken)
    {
        var user = await this._userRepository
            .GetByEmailAsync(
                loginUserDto.Email,
                cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var isPasswordValid = this._passwordHasher
            .VerifyPassword(
                user.PasswordHash,
                loginUserDto.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var token = this._jwtProvider
            .GenerateToken(user);

        return token;
    }
}
