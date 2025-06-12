using BL.Api;
using BL.Models;
using Dal.Api;
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

        private readonly IBLCategory category;
        private readonly IBLSubCategory subCategory;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IBL bl, ILogger<CategoryController> logger)
        {
            category = bl.BLCategory;
            subCategory = bl.BLSubCategory;
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

        [HttpGet]
        [Route("{categoryId}/subcategories")]
        public async Task<ActionResult<List<SubCategory>>> GetSubCategoriesByCategoryId(int categoryId)
        {
            try
            {
                var subCategories = await subCategory.GetSubCategoriesByCategoryAsync(categoryId);
                if (subCategories == null || subCategories.Count == 0)
                    return NotFound($"No subcategories found for categoryId {categoryId}.");

                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving subcategories for categoryId {categoryId}.");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}

