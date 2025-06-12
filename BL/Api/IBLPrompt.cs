using BL.Models;
using Dal.Entities;
using Microsoft.Identity.Client;

namespace BL.Api;

public interface IBLPrompt
{
    Task<List<Dal.Entities.Prompt>> GetPromptsByIdBl(int userId);
    Task CreatPrompt(LessonRequest lessonRequest, string generatedLessonContent);
}
