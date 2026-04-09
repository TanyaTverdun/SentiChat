using SentiChat.Application.Constants;

namespace SentiChat.Application.Extensions;

/// <summary>
/// Provides extension methods for string manipulation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Generates up to two uppercase initials 
    /// from a given string (e.g., a user's full name).
    /// </summary>
    /// <param name="name">
    /// The input string to generate initials from.
    /// </param>
    /// <returns>
    /// A string containing the initials. If the input is null or empty, 
    /// returns the default initials defined 
    /// in <see cref="ChatConstants.DefaultInitials"/>.
    /// </returns>
    public static string ToInitials(this string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return ChatConstants.DefaultInitials;
        }

        var parts = name.Trim().Split(
            ' ', 
            StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return char.ToUpper(parts[0][0]).ToString();
        }

        return string.Concat(
            char.ToUpper(parts[0][0]).ToString(), 
            char.ToUpper(parts[1][0]).ToString());
    }
}
