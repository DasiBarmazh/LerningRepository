using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BL.Api;
using BL.Models;
using Microsoft.Extensions.Options;

namespace BL.Services;

public class OpenAIService : IOpenAI
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _modelName;
    private readonly string _path;

    public OpenAIService(HttpClient httpClient, IOptions<OpenAISettings> openAISettings)
    {
        _apiKey = openAISettings.Value.ApiKey;
        _modelName = openAISettings.Value.ModelName;
        _httpClient = httpClient; 
        _path = openAISettings.Value.Path ;
    }

    public async Task<string> GetLessonAsync(string category, string subCategory, string userPrompt)
    {

        if (string.IsNullOrEmpty(_apiKey))
            throw new InvalidOperationException("מפתח ה-API של OpenAI לא הוגדר.");
        string fullPrompt = $"המשתמש בחר ללמוד על נושא: '{category}' ותת-נושא: '{subCategory}'. {userPrompt}";

        var request = new HttpRequestMessage(HttpMethod.Post, _path);
        request.Headers.Add("Authorization", $"Bearer {_apiKey}");

        var content = new
        {
            model = _modelName,
            messages = new[]
            {
                new { role = "system", content = "אתה עוזר לימודי שמסביר נושאים בצורה ברורה וקצרה." },
                new { role = "user", content = fullPrompt }
            },
            max_tokens = 1500
        };

        request.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string responseContent = await response.Content.ReadAsStringAsync();

        using (JsonDocument doc = JsonDocument.Parse(responseContent))
        {
            if (doc.RootElement.TryGetProperty("choices", out JsonElement choicesElement) &&
                choicesElement.ValueKind == JsonValueKind.Array &&
                choicesElement.GetArrayLength() > 0)
            {
                JsonElement firstChoice = choicesElement[0];
                if (firstChoice.TryGetProperty("message", out JsonElement messageElement) &&
                    messageElement.TryGetProperty("content", out JsonElement contentResultElement))
                {
                    return contentResultElement.GetString();
                }
            }
            throw new JsonException("הפענוח של תגובת OpenAI נכשל: 'choices[0].message.content' לא נמצא או המבנה אינו חוקי.");
        }
    }
}