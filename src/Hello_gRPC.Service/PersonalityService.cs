
using HelloGrpc.Data;
using HelloGrpc.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HelloGrpc.Service;

public class PersonalityService(AppDbContext dbContext)
{
    public async Task<List<Personality>> GetAllAsync() => await dbContext.Personalities.ToListAsync();
    public async Task<Personality?> GetByIdAsync(int id) => await dbContext.Personalities.FindAsync(id);
    public async Task<Personality> AddAsync(Personality personality)
    {
        dbContext.Personalities.Add(personality);
        await dbContext.SaveChangesAsync();
        return personality;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await dbContext.Personalities.FindAsync(id);
        if (entity is null) return false;
        dbContext.Personalities.Remove(entity);
        await dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<Personality> UpdateAsync(Personality personality)
    {
        dbContext.Personalities.Update(personality);
        await dbContext.SaveChangesAsync();
        return personality;
    }
}
