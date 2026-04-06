using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SentiChat.Domain.Entities;

namespace SentiChat.Infrastructure.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .HasKey(m => m.Id);

        builder
            .Property(m => m.Text)
            .HasMaxLength(4000)
            .IsUnicode(true)
            .IsRequired();

        builder
            .Property(m => m.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(m => m.SentimentResult)
            .HasMaxLength(50)
            .IsUnicode(false);

        builder
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(m => m.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
