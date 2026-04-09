namespace SentiChat.Application.Constants;

/// <summary>
/// Contains constant string values for 
/// SignalR client events to avoid magic strings.
/// </summary>
public static class SignalRConstants
{
    /// <summary>
    /// The event name triggered on the client 
    /// when a new message is received.
    /// </summary>
    public const string ReceiveMessage = "ReceiveMessage";

    /// <summary>
    /// The event name triggered on the client 
    /// when a user goes online or offline.
    /// </summary>
    public const string UserStatusChanged = "UserStatusChanged";

    /// <summary>
    /// The event name triggered on the client 
    /// when a user starts or stops typing.
    /// </summary>
    public const string UserTyping = "UserTyping";

    /// <summary>
    /// The query parameter name used to pass the JWT token 
    /// during the initial SignalR WebSocket connection negotiation.
    /// </summary>
    public const string AccessTokenQueryParam = "access_token";

    /// <summary>
    /// The base URL path where the SignalR hubs are mapped.
    /// </summary>
    public const string HubBasePath = "/hubs";

    /// <summary>
    /// The configuration key path used to 
    /// retrieve the Azure SignalR connection string 
    /// from the application settings (e.g., appsettings.json).
    /// </summary>
    public const string AzureConnectionStringConfigPath = 
        "Azure:SignalR:ConnectionString";
}
