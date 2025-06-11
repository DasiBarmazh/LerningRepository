using BL.Api;
using BL.Models;
using Dal.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LerningPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        IBLCategory category;
        private readonly ILogger<CategoryController> _logger; 
        public CategoryController(IBL bl, ILogger<CategoryController> logger)
        {
            category = bl.BLCategory;
            _logger = logger;
        }
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                var categories = await category.GetAllCategoriesAsync();

                if (categories == null || categories.Count == 0)
                    return NotFound("No categories found.");

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving categories.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
