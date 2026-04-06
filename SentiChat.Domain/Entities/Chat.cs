namespace SentiChat.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsGroup { get; set; }

    public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
