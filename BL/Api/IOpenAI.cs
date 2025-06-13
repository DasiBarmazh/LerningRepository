namespace BL.Api;

public interface IOpenAI
{
    Task<string> GetLessonAsync(string category, string subCategory, string userPrompt);
}
