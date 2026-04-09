namespace SentiChat.Application.Constants;

/// <summary>
/// Contains constant string values for SignalR client events to avoid magic strings.
/// </summary>
public static class SignalRConstants
{
    public const string ReceiveMessage = "ReceiveMessage";
    public const string UserStatusChanged = "UserStatusChanged";
    public const string UserTyping = "UserTyping";

    public const string AccessTokenQueryParam = "access_token";
    public const string HubBasePath = "/hubs";

    public const string AzureConnectionStringConfigPath = 
        "Azure:SignalR:ConnectionString";
}
