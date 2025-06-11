using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LerningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromptController : ControllerBase
    {
        IOpenAI OpenAI;

        private readonly ILogger<PromptController> _logger;
        public PromptController(IBL bl, ILogger<PromptController> logger)
        {
            OpenAI = bl.OpenAI;
            _logger = logger;
        }
        [HttpPost("lesson")]
        public async Task<IActionResult> GetLesson([FromBody] LessonRequest request)
        {
            try
            {
                var lesson = await OpenAI.GetLessonAsync(request.Category, request.SubCategory, request.UserPrompt);
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

    }
}
