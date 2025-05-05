using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110KRTERMPROJ.Services;

public class DbSkillRepository : ISkillRepository
{
    private readonly ApplicationDbContext _db;

    public DbSkillRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    //Reads a specific Skill and loads collection of roles
    public async Task<Skill?> ReadAsync(int id)
    {
        return await _db.Skills
            .Include(s => s.RolesWithThisSkill)
                .ThenInclude(skr => skr.Role)
            .FirstOrDefaultAsync(s => s.SkillId == id);
    }

    //Read all skills and include collections for assigned (linked) persons and roles
    public async Task<ICollection<Skill>> ReadAllAsync()
    {
        return await _db.Skills
            .Include(s => s.RolesWithThisSkill)
                .ThenInclude(skr => skr.Role)
            .Include(sp => sp.PersonsWithThisSkill)
                .ThenInclude(skp => skp.Person)
            .ToListAsync();
    }

    //Read all skills and include collection for assigned (linked) roles
    public async Task<ICollection<Skill>> ReadAllForRoleAsync()
    {
        return await _db.Skills
            .Include(s => s.RolesWithThisSkill)
                .ThenInclude(skr => skr.Role)
            .ToListAsync();
    }

    //Read all skills and include collection for assigned (linked) persons
    public async Task<ICollection<Skill>> ReadAllForPersonAsync()
    {
        return await _db.Skills
            .Include(s => s.PersonsWithThisSkill)
                .ThenInclude(skp => skp.Person)
            .ToListAsync();
    }

    //Create skill
    public async Task<Skill> CreateAsync(Skill skill)
    {
        await _db.Skills.AddAsync(skill);

        await _db.SaveChangesAsync();

        return skill;

    }

    public async Task<Skill> GetByIdAsync(int id)
    {
        return await _db.Skills.FindAsync(id);
    }

    //Loan specific skill and include collections for assigned (linked) persons and roles
    public async Task<Skill?> GetByIdDetailsAsync(int id)
    {
        //This reads a specific Skill and its associations
        //Eager load the skills's roles and then eager load the Role
        return await _db.Skills
            .Include(s => s.RolesWithThisSkill)
                .ThenInclude(skr => skr.Role)
            .Include(s => s.PersonsWithThisSkill)
                .ThenInclude(skp => skp.Person)
            .FirstOrDefaultAsync(s => s.SkillId == id);
    }

    //Update skill
    public async Task<Skill> UpdateAsync(Skill skill)
    {
        _db.Skills.Update(skill);
        await _db.SaveChangesAsync();
        return skill; //could be wrong
    }

    //Delete skill
    public async Task DeleteAsync(int id)
    {
        Skill? skillToDelete = await GetByIdAsync(id);
        if (skillToDelete != null)
        {
            _db.Skills.Remove(skillToDelete);
            await _db.SaveChangesAsync();
        }
    }
}
