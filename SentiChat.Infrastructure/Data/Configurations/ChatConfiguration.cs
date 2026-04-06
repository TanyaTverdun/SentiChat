using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentiChat.Domain.Entities;

namespace SentiChat.Infrastructure.Data.Configurations;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Title)
            .HasMaxLength(100)
            .IsUnicode(true);

        builder
            .Property(c => c.IsGroup)
            .IsRequired();
    }
}
