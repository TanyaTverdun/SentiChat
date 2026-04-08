using SentiChat.Domain.Enums;

namespace SentiChat.Application.Interfaces.AzureAI
{
    /// <summary>
    /// Defines a contract for analyzing the emotional tone of text.
    /// </summary>
    public interface ISentimentAnalysisService
    {
        /// <summary>
        /// Analyzes the sentiment of the provided text.
        /// </summary>
        /// <param name="text">The message content to analyze.</param>
        /// <returns>The detected sentiment type (Positive, Negative, Neutral, or Mixed).</returns>
        Task<SentimentType> AnalyzeSentimentAsync(string text);
    }
}
