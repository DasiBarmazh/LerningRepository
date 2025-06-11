using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Services;

public class CategoryService : ICategory
{
    private readonly LearningPlatformContext db;

    public CategoryService(LearningPlatformContext db)
    {
        this.db = db;
    }

    public async Task Create(Category item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existsCategory = await db.Categories.AnyAsync(c => c.Name == item.Name);
        if (existsCategory)
            throw new InvalidOperationException("Category already exists.");

        await db.Categories.AddAsync(item);
        await db.SaveChangesAsync();
    }

    public async Task Delete(Category item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existingCategory = await db.Categories.FindAsync(item.Id);
        if (existingCategory == null)
            throw new InvalidOperationException("Category not found.");

        db.Categories.Remove(existingCategory);
        await db.SaveChangesAsync();
    }

    public async Task<List<Category>> Read()
    {
        return await db.Categories
            .ToListAsync();
    }

    public Task Update(Category item)
    {
        throw new NotImplementedException("Update(User) is not supported for CategoryService.");
    }
}
