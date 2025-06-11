using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Api;
using Dal.Api;
using Dal.Entities;

namespace BL.Services;

public class BLSubCategoryService : IBLSubCategory
{
    ISubCategory  dalSubCategory;
    public BLSubCategoryService(IDal dal)
    {
        dalSubCategory = dal.subCategory;
    }
    public async Task<List<SubCategory>> GetSubCategoriesByCategoryAsync(int categoryId)
    {
        return await dalSubCategory.GetByCategoryIdAsync(categoryId);
    }
}
