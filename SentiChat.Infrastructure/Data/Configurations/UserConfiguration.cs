using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentiChat.Domain.Entities;

namespace SentiChat.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Name)
            .HasMaxLength(100)
            .IsUnicode(true)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder
            .Property(u => u.Bio)
            .HasMaxLength(500)
            .IsUnicode(true);

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
