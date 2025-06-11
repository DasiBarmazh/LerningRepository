using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace Dal.Api;

public interface ISubCategory : Icurd<SubCategory>
{
    Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId);
}
