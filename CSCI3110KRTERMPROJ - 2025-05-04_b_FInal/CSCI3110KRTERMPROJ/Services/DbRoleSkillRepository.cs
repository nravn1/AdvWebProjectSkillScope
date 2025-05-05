using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110KRTERMPROJ.Services;

public class DbRoleSkillRepository : IRoleSkillRepository
{
    private readonly ApplicationDbContext _db;

    public DbRoleSkillRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<RoleSkill?> ReadAsync(int id)
    {
        //This reads a specific RoleSkill and its associations
        //Eager load the roles and then eager load the Skills
        return await _db.RoleSkills
            .Include(rsk => rsk.Role)
            .Include(rsk => rsk.Skill)
            .FirstOrDefaultAsync(rsk => rsk.Id == id);
    }
    public async Task<ICollection<RoleSkill>> ReadAllAsync()
    {
        //return await _db.Skills.ToListAsync();
        //This reads a all RoleSkills and its associations
        //Eager load the roles and then eager load the Skills
        return await _db.RoleSkills
            .Include(rsk => rsk.Role)
            .Include(rsk => rsk.Skill)
            .ToListAsync();
    }

    public async Task<RoleSkill> CreateAsync(RoleSkill roleskill)
    {
        await _db.RoleSkills.AddAsync(roleskill);

        await _db.SaveChangesAsync();

        return roleskill;

    }

    public async Task<RoleSkill> GetByIdAsync(int id)
    {
        return await _db.RoleSkills.FindAsync(id);
    }

    public async Task<RoleSkill> UpdateAsync(RoleSkill roleskill)
    {
        _db.RoleSkills.Update(roleskill);
        await _db.SaveChangesAsync();
        return roleskill; //could be wrong
    }

    public async Task DeleteAsync(int id)
    {
        RoleSkill? roleskillToDelete = await GetByIdAsync(id);
        if (roleskillToDelete != null)
        {
            _db.RoleSkills.Remove(roleskillToDelete);
            await _db.SaveChangesAsync();
        }
    }
}
