using BL.Api;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LerningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        IOpenAI OpenAI;
        IBLPrompt prompt;

        private readonly ILogger<PromptController> _logger;
        public PromptController(IBL bl, ILogger<PromptController> logger)
        {
            OpenAI = bl.OpenAI;
            _logger = logger;
            prompt = bl.prompt;
        }
        
        [HttpPost("lesson")]
        public async Task<IActionResult> GetLesson([FromBody] LessonRequest request)
        {

            try
            {
                 var lesson = await OpenAI.GetLessonAsync(request.Category.Name, request.SubCategory.Name, request.UserPrompt);
                if (string.IsNullOrWhiteSpace(lesson))
                {
                    _logger.LogWarning("OpenAI returned an empty or whitespace lesson for request: Category={Category}, SubCategory={SubCategory}, Prompt={Prompt}",
                                       request.Category?.Name, request.SubCategory?.Name, request.UserPrompt);
                    return StatusCode(500, new { error = "המערכת לא הצליחה ליצור שיעור כרגע. נסה שוב." }); // הודעה למשתמש
                }

                await prompt.CreatPrompt(request, lesson);

                return Ok(new { lesson });
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                _logger.LogError(ex, "OpenAI rate limit exceeded");
                return StatusCode(429, new { error = "המערכת עמוסה כרגע. נסה שוב בעוד מספר שניות." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLesson");
                return StatusCode(500, new { error = ex.Message });
            }
        }
        [HttpGet("user/{userId}/prompts")]
        public async Task<IActionResult> GetUserPrompts(int userId)
        {
            if (userId <= 0)
            {
                _logger.LogWarning("Invalid userId received: {UserId}", userId);
                return BadRequest(new { error = "Invalid user ID." });
            }

            try
            {
                var prompts = await prompt.GetPromptsByIdBl(userId);

                if (prompts == null || prompts.Count == 0)
                {
                    _logger.LogInformation("No prompts found for user {UserId}", userId);
                    return NotFound(new { error = "No prompts found for this user." });
                }

                return Ok(prompts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching prompts for user {UserId}", userId);
                return StatusCode(500, new { error = "Failed to fetch prompts." });
            }
        }
    }
}
