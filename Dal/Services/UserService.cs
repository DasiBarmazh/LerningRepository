using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using Dal.Entities;

namespace Dal.Services;

using Microsoft.EntityFrameworkCore;

public class UserService : IUser
{
    private readonly LearningPlatformContext db;
    public UserService(LearningPlatformContext db)
    {
        this.db = db;
    }

    public async Task Create(User item)
    {
        try
        {

            var existingUser = await db.Users.FirstOrDefaultAsync(c => c.Phone == item.Phone);
            Console.WriteLine(existingUser);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");

            }
            await db.Users.AddAsync(item);
            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create user: " + ex.Message, ex);
        }
    }

    public async Task Delete(User item)
    {
        db.Users.Remove(item);
        await db.SaveChangesAsync();
    }

    public async Task<List<User>> Read()
    {
        return await db.Users.ToListAsync();
    }

    public async Task Update(User item)
    {
        var existingClient = await db.Users.FirstOrDefaultAsync(e => e.Id == item.Id);
        if (existingClient == null) return;
        existingClient.Name = item.Name;
        existingClient.Phone = item.Phone;

        await db.SaveChangesAsync();
    }

    public async Task<User?> GetUserById(string username, string phone)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new InvalidOperationException("Data transfer error, please try again.");
        }
        return await db.Users.FirstOrDefaultAsync(e => e.Phone == phone && e.Name == username);
    }
}
