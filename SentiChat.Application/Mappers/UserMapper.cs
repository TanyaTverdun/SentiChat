using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Users;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Mappers;

[Mapper]
public static partial class UserMapper
{
    [MapperIgnoreTarget(nameof(User.Bio))]
    [MapperIgnoreTarget(nameof(User.SentMessages))]
    [MapperIgnoreTarget(nameof(User.ChatMembers))]
    [MapperIgnoreSource(nameof(RegisterUserDto.Password))]
    public static partial User ToEntity(this RegisterUserDto dto, Guid id, string passwordHash);
}
