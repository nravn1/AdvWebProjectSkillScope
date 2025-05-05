using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Services;

public class DbRoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _db;

    public DbRoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    //Read specific roles and include collection for skills
    public async Task<Role?> ReadAsync(int id)
    {
        return await _db.Roles
            .Include(r => r.SkillsOfaRole)
                .ThenInclude(r => r.Skill)
            .FirstOrDefaultAsync(r => r.RoleId == id);
    }

    //Read all roles and include collections for skills and persons
    public async Task<ICollection<Role>> ReadAllAsync()
    {
        return await _db.Roles
            .Include(r => r.SkillsOfaRole)
                .ThenInclude(r => r.Skill)
            .Include(rp => rp.PersonsWithRole)
                .ThenInclude(rp => rp.Person)
            .ToListAsync();
    }

    //Read all roles and include collections for persons
    public async Task<ICollection<Role>> ReadAllForPersonAsync()
    {
        Debug.WriteLine("<<..debug..>> DbRoleRepository.cs/ReadAllForPersonAsync:");

        return await _db.Roles
            .Include(r => r.PersonsWithRole)
                .ThenInclude(p => p.Person)
            //.Include(r => r.PersonsWithRole)
            .ToListAsync();
    }

    // Assign an skill to a role - called from Role Controller
    public async Task<AssignmentStatus> AssignSkillToRoleAsync(int roleId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> DbRoleRepository.cs/AssignSkilltoRoleAsync:  roleId=" + roleId + "  skillId=" + skillId);

        var role = await _db.Roles
            .Include(r => r.SkillsOfaRole)
            .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
            return AssignmentStatus.NoRoleExists;

        // Check if already assigned
        if (role.SkillsOfaRole.Any(rs => rs.SkillId == skillId))
            return AssignmentStatus.LinkAlreadyExists;

        // Check if skill exists
        var skill = await _db.Skills
            .Include(s => s.RolesWithThisSkill)
            .FirstOrDefaultAsync(s => s.SkillId == skillId);

        if (skill == null)
            return AssignmentStatus.NoSkillExists;

        // Insert role/skill assignment
        var roleSkill = new RoleSkill
        {
            RoleId = roleId,
            SkillId = skillId
        };

        role.SkillsOfaRole.Add(roleSkill);
        skill.RolesWithThisSkill.Add(roleSkill);

        await _db.SaveChangesAsync();
        return AssignmentStatus.Success;
    }

    // Unassign an skill from a role - called from Role Controller
    public async Task<AssignmentStatus> UnAssignSkillFromRoleAsync(int roleId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> DbRoleRepository.cs/UnAssignSkillFromRoleAsync:  roleId=" + roleId + "  skillId=" + skillId);

        var role = await _db.Roles
            .Include(r => r.SkillsOfaRole)
            .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
            return AssignmentStatus.NoRoleExists;

        // Check if skill exists
        var skill = await _db.Skills
            .Include(s => s.RolesWithThisSkill)
            .FirstOrDefaultAsync(s => s.SkillId == skillId);

        if (skill == null)
            return AssignmentStatus.NoSkillExists;

        // Delete role/skill assignment link
        // Retrieve the RoleSkill object to be deleted using specific roleId and skillId
        var roleSkill = await _db.RoleSkills
            .Include(rs => rs.Role)   // Include related Role
            .Include(rs => rs.Skill)  // Include related Skill
            .FirstOrDefaultAsync(rs => rs.RoleId == roleId && rs.SkillId == skillId);

        //If RoleSkill object exists, remove it
        if (roleSkill != null)
        {
            // Remove the roleSkill from both navigation properties if they exist
            _db.RoleSkills.Remove(roleSkill);
            if (roleSkill.Role != null)
            {
                roleSkill.Role.SkillsOfaRole.Remove(roleSkill);
            }
            if (roleSkill.Skill != null)
            {
                roleSkill.Skill.RolesWithThisSkill.Remove(roleSkill);
            }

            // Save changes to the database
            await _db.SaveChangesAsync();
            return AssignmentStatus.Success;
        }
        else
        {
            return AssignmentStatus.LinkDoesNotExist;
        }
    }

    public async Task<Role?> ReadAsyncExplicitLoad(int id)
    {
        //This reads a specific Role and its associations
        //Explicit load the roles's skills
        var role = await _db.Roles.FindAsync(id);
        if (role != null)
        {
            _db.Entry(role)
                .Collection(r => r.SkillsOfaRole)
                .Load();
        }
        return role;
    }

    //Creat role
    public async Task<Role> CreateAsync(Role role)
    {
        await _db.Roles.AddAsync(role);

        await _db.SaveChangesAsync();

        return role;

    }

    public async Task<Role> GetByIdAsync(int id)
    {
        return await _db.Roles.FindAsync(id);
    }

    //Read specific roles by id and include collections for skills and persons
    public async Task<Role> GetByIdDetailsAsync(int id)
    {
        return await _db.Roles
            .Include(r => r.SkillsOfaRole)
                .ThenInclude(r => r.Skill)
            .Include(rp => rp.PersonsWithRole)
                .ThenInclude(rp => rp.Person)
            .FirstOrDefaultAsync(r => r.RoleId == id);

    }

    //Update role
    public async Task<Role> UpdateAsync(Role role)
    {
        _db.Roles.Update(role);
        await _db.SaveChangesAsync();
        return role;
    }

    //Delete role
    public async Task DeleteAsync(int id)
    {
        Role? roleToDelete = await GetByIdAsync(id);
        if (roleToDelete != null)
        {
            _db.Roles.Remove(roleToDelete);
            await _db.SaveChangesAsync();
        }
        
    }
}
