using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Services;

public class SubCategoryService : ISubCategory
{
    private readonly LearningPlatformContext db;

    public SubCategoryService(LearningPlatformContext db)
    {
        this.db = db;
    }

    public async Task Create(SubCategory item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var exists = await db.SubCategories.AnyAsync(s => s.Name == item.Name && s.CategoryId == item.CategoryId);
        if (exists)
            throw new InvalidOperationException("SubCategory already exists in this category.");

        await db.SubCategories.AddAsync(item);
        await db.SaveChangesAsync();
    }

    public async Task Delete(SubCategory item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existing = await db.SubCategories.FindAsync(item.Id);
        if (existing == null)
            throw new InvalidOperationException("SubCategory not found.");

        db.SubCategories.Remove(existing);
        await db.SaveChangesAsync();
    }

    public async Task<List<SubCategory>> Read()
    {
        return await db.SubCategories
            .Include(s => s.Category)
            .ToListAsync();
    }

    public async Task Update(SubCategory item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existing = await db.SubCategories.FindAsync(item.Id);
        if (existing == null)
            throw new InvalidOperationException("SubCategory not found.");

        existing.Name = item.Name;
        existing.CategoryId = item.CategoryId;
        await db.SaveChangesAsync();
    }

    public async Task<List<SubCategory>> GetByCategoryIdAsync(int categoryId)
    {
        return await db.SubCategories
            .Where(sc => sc.CategoryId == categoryId)
            .ToListAsync();
    }

}