using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace BL.Api;

public interface IBLCategory
{
    Task<List<Category>> GetAllCategoriesAsync();
}
