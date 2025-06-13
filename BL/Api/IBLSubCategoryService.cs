using Dal.Entities;

namespace BL.Api;

public interface IBLSubCategory
{
    Task<List<SubCategory>> GetSubCategoriesByCategoryAsync(int categoryId);
}
