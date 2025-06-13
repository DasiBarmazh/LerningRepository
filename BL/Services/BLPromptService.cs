using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Entities;
using Microsoft.Identity.Client;

namespace BL.Services;

public class BLPromptService : IBLPrompt
{
    IPrompt dalPrompt;
    public BLPromptService(IDal dal)
    {
        dalPrompt = dal.prompt;
    }
    public async Task CreatPrompt(LessonRequest lessonRequest, string generatedLessonContent)
    {
        if (lessonRequest == null)
        {
            throw new ArgumentNullException(nameof(lessonRequest), "Lesson request cannot be null.");
        }

        var promptToSave = new Dal.Entities.Prompt
        {
            UserId = lessonRequest.UserId,
            CategoryId = lessonRequest.Category.Id,
            SubCategoryId = lessonRequest.SubCategory.Id,
            Prompt1 = lessonRequest.UserPrompt,     
            Response = generatedLessonContent, 
            CreatedAt = DateTime.UtcNow     
        };

        await dalPrompt.Create(promptToSave); 

    }
    public async Task<List<Dal.Entities.Prompt>> GetPromptsByIdBl(int userId)
    {
        return await dalPrompt.GetPromptsByUserIdAsync(userId);
    }
}