using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Api;
using Dal.Api;
using Dal.Entities;

namespace BL.Services;

public class BLCategoryService : IBLCategory
{
    ICategory dalcategory;

    public BLCategoryService(IDal dal)
    {
        dalcategory = dal.category;
    }
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        try
        {
            var categories = await dalcategory.Read();
            if (categories == null)
                return new List<Category>();

            return categories.ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving categories.", ex);
        }
    }

}
