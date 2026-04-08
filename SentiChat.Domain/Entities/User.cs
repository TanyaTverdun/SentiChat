namespace SentiChat.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Bio { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsOnline { get; set; } = false;
    public DateTime? LastSeen { get; set; }

    public ICollection<ChatMember> ChatMembers { get; set; } = new List<ChatMember>();
    public ICollection<Message> SentMessages { get; set; } = new List<Message>();
}
