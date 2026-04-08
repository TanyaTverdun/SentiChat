namespace SentiChat.Domain.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool IsGroup { get; set; }

    public ICollection<ChatMember> Members { get; set; } = new List<ChatMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public static Chat CreatePersonal(
        Guid user1Id,
        Guid user2Id)
    {
        var chat = new Chat
        {
            Id = Guid.NewGuid(),
            IsGroup = false,
            Title = null
        };

        chat.Members.Add(new ChatMember
        {
            ChatId = chat.Id,
            UserId = user1Id
        });

        chat.Members.Add(new ChatMember
        {
            ChatId = chat.Id,
            UserId = user2Id
        });

        return chat;
    }
}
