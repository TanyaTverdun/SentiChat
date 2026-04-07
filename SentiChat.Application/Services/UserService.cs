using SentiChat.Application.DTOs.Users;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Application.Mappers;
using SentiChat.Domain.Interfaces.Repositories;

namespace SentiChat.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher)
    {
        this._unitOfWork = unitOfWork;
        this._userRepository = userRepository;
        this._passwordHasher = passwordHasher;
    }

    public async Task<Guid> RegisterUserAsync(
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

        return userId;
    }
}
