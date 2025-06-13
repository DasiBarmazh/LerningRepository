using Dal.Entities;

namespace BL.Api;

public interface IBLCategory
{
    Task<List<Category>> GetAllCategoriesAsync();
}
