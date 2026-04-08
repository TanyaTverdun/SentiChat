namespace SentiChat.Application.DTOs.Chats;

public class ChatListItemDto
{
    public Guid ChatId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Initials { get; set; } = string.Empty;
    public string? LastMessageText { get; set; }
    public DateTime? LastMessageTime { get; set; }
}
