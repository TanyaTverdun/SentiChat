namespace SentiChat.Application.Constants;

/// <summary>
/// Contains default and fallback constant values 
/// used throughout the chat domain.
/// </summary>
public static class ChatConstants
{
    /// <summary>
    /// The fallback name displayed when a chat has no explicit title.
    /// </summary>
    public const string DefaultChatName = "Невідомий чат";

    /// <summary>
    /// The fallback initials displayed in the avatar placeholder 
    /// when a chat name is missing or cannot be parsed.
    /// </summary>
    public const string DefaultInitials = "НЧ";
}
