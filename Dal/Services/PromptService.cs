using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Services;

public class PromptService : IPrompt
{
    private readonly LearningPlatformContext db;

    public PromptService(LearningPlatformContext db)
    {
        this.db = db;
    }

    public async Task Create(Prompt item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var exists = await db.Prompts.AnyAsync(p =>
            p.UserId == item.UserId &&
            p.CategoryId == item.CategoryId &&
            p.SubCategoryId == item.SubCategoryId &&
            p.Prompt1 == item.Prompt1);

        if (!exists) 
        {
            item.CreatedAt = DateTime.UtcNow;
            await db.Prompts.AddAsync(item);
            await db.SaveChangesAsync();
        }

    }

    public async Task Delete(Prompt item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existing = await db.Prompts.FindAsync(item.Id);
        if (existing == null)
            throw new InvalidOperationException("Prompt not found.");

        db.Prompts.Remove(existing);
        await db.SaveChangesAsync();
    }

    public async Task<List<Prompt>> Read()
    {
        return await db.Prompts
            .Include(p => p.User)
            .Include(p => p.Category)
            .Include(p => p.SubCategory)
            .ToListAsync();
    }

    public async Task Update(Prompt item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        var existing = await db.Prompts.FindAsync(item.Id);
        if (existing == null)
            throw new InvalidOperationException("Prompt not found.");

        existing.Prompt1 = item.Prompt1;
        existing.Response = item.Response;
        existing.CategoryId = item.CategoryId;
        existing.SubCategoryId = item.SubCategoryId;
        existing.UserId = item.UserId;

        await db.SaveChangesAsync();
    }

    public async Task<List<Prompt>> GetPromptsByUserIdAsync(int userId)
    {
        return await db.Prompts
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }
}