using Microsoft.EntityFrameworkCore;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Enums;

namespace SentiChat.Infrastructure.Data;

public class SentiChatDbInitializer
{
    private readonly SentiChatDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public SentiChatDbInitializer(
        SentiChatDbContext context,
        IPasswordHasher passwordHasher)
    {
        this._context = context;
        this._passwordHasher = passwordHasher;
    }

    public async Task SeedAsync()
    {
        await this._context.Database.MigrateAsync();

        await this._context.Messages.ExecuteDeleteAsync();
        await this._context.ChatMembers.ExecuteDeleteAsync();
        await this._context.Chats.ExecuteDeleteAsync();
        await this._context.Users.ExecuteDeleteAsync();

        var user1Id = Guid.NewGuid();
        var user2Id = Guid.NewGuid();
        var user3Id = Guid.NewGuid();

        var defaultPassword = this._passwordHasher.HashPassword("Password123!");

        var users = new List<User>
        {
            new User
            {
                Id = user1Id, 
                Name = "Олена Шевченко", 
                Email = "olena@test.com",
                Bio = "UI/UX дизайнерка", 
                PasswordHash = defaultPassword, 
                IsOnline = true, 
                LastSeen = DateTime.UtcNow
            },
            new User
            {
                Id = user2Id, 
                Name = "Максим Коваленко", 
                Email = "maksym@test.com",
                Bio = ".NET Розробник", 
                PasswordHash = defaultPassword, 
                IsOnline = false, 
                LastSeen = DateTime.UtcNow.AddHours(-2)
            },
            new User
            {
                Id = user3Id, 
                Name = "Анна Бойко", 
                Email = "anna@test.com",
                Bio = "QA Engineer", 
                PasswordHash = defaultPassword, 
                IsOnline = true, 
                LastSeen = DateTime.UtcNow
            }
        };

        await this._context.Users.AddRangeAsync(users);

        var chatOlenaMaksym = Chat.CreatePersonal(
            user1Id, 
            user2Id);

        var chatOlenaAnna = Chat.CreatePersonal(
            user1Id, 
            user3Id);

        var chatMaksymAnna = Chat.CreatePersonal(
            user2Id, 
            user3Id);

        await this._context.Chats.AddRangeAsync(
            chatOlenaMaksym, 
            chatOlenaAnna, 
            chatMaksymAnna);

        var allMessages = new List<Message>();

        string[] samplePhrases = {
            "Привіт! Як справи?", "Все чудово, працюю. А ти?", "Теж в роботі",
            "Є питання по нашому проєкту.", "Давай, уважно слухаю.", "Скинув файли на пошту.",
            "Дякую, бачу. Виглядає круто!", "Трохи втомився, піду вип'ю кави",
            "Смачного!", "Повернувся. Продовжимо?"
        };

        void GenerateMessages(Chat chat, Guid userA, Guid userB)
        {
            var startDate = DateTime.UtcNow.AddDays(-2);

            for (int i = 1; i <= 60; i++)
            {
                var senderId = (i % 2 != 0) ? userA : userB;

                var text = $"{samplePhrases[i % samplePhrases.Length]}";

                var sentiment = (SentimentType)(i % 4);

                allMessages.Add(new Message
                {
                    Id = Guid.NewGuid(),
                    ChatId = chat.Id,
                    SenderId = senderId,
                    Text = text,
                    CreatedAt = startDate.AddMinutes(i * 15),
                    Sentiment = sentiment
                });
            }
        }

        GenerateMessages(chatOlenaMaksym, user1Id, user2Id);
        GenerateMessages(chatOlenaAnna, user1Id, user3Id);
        GenerateMessages(chatMaksymAnna, user2Id, user3Id);

        await this._context.Messages.AddRangeAsync(allMessages);

        await this._context.SaveChangesAsync();
    }
}