using SentiChat.Application.Interfaces.AzureAI;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using SentiChat.Domain.Enums;

namespace SentiChat.Infrastructure.ExternalServices.AzureAI;

public class SentimentAnalysisService : ISentimentAnalysisService
{
    private readonly TextAnalyticsClient _client;

    public SentimentAnalysisService(IConfiguration configuration)
    {
        var endpoint = configuration["Azure:LanguageService:Endopoint"];
        var apiKey = configuration["Azure:LanguageService:ApiKey"];

        if(string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException(
                "Azure AI settings are missing in configuration.");
        }

        var credentials = new AzureKeyCredential(apiKey);
        this._client = new TextAnalyticsClient(
            new Uri(endpoint), 
            credentials);
    }

    public async Task<SentimentType> AnalyzeSentimentAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return SentimentType.Neutral;
        }

        try
        {
            DocumentSentiment response = await this._client
                .AnalyzeSentimentAsync(text);

            return response.Sentiment switch
            {
                TextSentiment.Positive => SentimentType.Positive,
                TextSentiment.Negative => SentimentType.Negative,
                TextSentiment.Neutral => SentimentType.Neutral,
                TextSentiment.Mixed => SentimentType.Mixed,
                _ => SentimentType.Neutral
            };
        }
        catch
        {
            return SentimentType.Neutral;
        }
    }
}
