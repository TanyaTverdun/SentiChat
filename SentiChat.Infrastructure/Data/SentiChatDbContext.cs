using Microsoft.EntityFrameworkCore;
using SentiChat.Domain.Entities;

namespace SentiChat.Infrastructure.Data;

public class SentiChatDbContext : DbContext
{
    public SentiChatDbContext(DbContextOptions<SentiChatDbContext> options) 
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatMember> ChatMembers => Set<ChatMember>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SentiChatDbContext).Assembly);
    }

}
