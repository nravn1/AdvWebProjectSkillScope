using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Services;

public class DbPersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _db;

    public DbPersonRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    //Read all persons and include collections for assigned (linked) privileges, skills and roles
    public async Task<ICollection<Person>> ReadAllAsync()
    {
        //return await _db.Persons.ToListAsync();
        return await _db.Persons
            .Include(p => p.PrivilegesOfaPerson)
                .ThenInclude(pp => pp.Privilege)
            .Include(p => p.SkillsOfaPerson)
                .ThenInclude(ps => ps.Skill)
            .Include(p => p.RolesOfaPerson)
                .ThenInclude(pr => pr.Role)
            .ToListAsync();
    }

    // Get a specific person with its related privileges
    public async Task<Person?> ReadAsync(int id)
    {
        return await _db.Persons
            .Include(p => p.PrivilegesOfaPerson)
            .ThenInclude(pp => pp.Privilege)
            .FirstOrDefaultAsync(p => p.PersonId == id);
    }

    // Assign an privilege to a person - called from Controller
    public async Task<AssignmentStatus> AssignPrivilegeToPersonAsync(int personId, int privilegeId)
    {
        var person = await _db.Persons
            .Include(p => p.PrivilegesOfaPerson)
            .FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if already assigned
        if (person.PrivilegesOfaPerson.Any(pp => pp.PrivilegeId == privilegeId))
            return AssignmentStatus.LinkAlreadyExists;

        var privilege = await _db.Privileges
            .Include(p => p.PersonsWithThisPrivilege)
            .FirstOrDefaultAsync(p => p.PrivilegeId == privilegeId);

        if (privilege == null)
            return AssignmentStatus.NoPrivilegeExists;

        var personPrivilege = new PersonPrivilege
        {
            PersonId = personId,
            PrivilegeId = privilegeId
        };

        person.PrivilegesOfaPerson.Add(personPrivilege);
        privilege.PersonsWithThisPrivilege.Add(personPrivilege);

        await _db.SaveChangesAsync();
        return AssignmentStatus.Success;
    }

    // Assign an skill to a person - called from Controller
    public async Task<AssignmentStatus> AssignSkillToPersonAsync(int personId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> DbPersonRepository.cs/AssignSkillToPersonAsync:  personId=" + personId + "  skillId=" + skillId);

        var person = await _db.Persons
            .Include(p => p.SkillsOfaPerson)
            .FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if already assigned
        if (person.SkillsOfaPerson.Any(pp => pp.SkillId == skillId))
            return AssignmentStatus.LinkAlreadyExists;

        var skill = await _db.Skills
            .Include(p => p.PersonsWithThisSkill)
            .FirstOrDefaultAsync(p => p.SkillId == skillId);

        if (skill == null)
            return AssignmentStatus.NoSkillExists;

        var personSkill = new PersonSkill
        {
            PersonId = personId,
            SkillId = skillId
        };

        person.SkillsOfaPerson.Add(personSkill);
        skill.PersonsWithThisSkill.Add(personSkill);

        await _db.SaveChangesAsync();
        return AssignmentStatus.Success;
    }

    // Assign a role to a person - called from Controller
    public async Task<AssignmentStatus> AssignRoleToPersonAsync(int personId, int roleId)
    {
        Debug.WriteLine("<<..debug..>> DbPersonRepository.cs/AssignRoleToPersonAsync:  personId=" + personId + "  roleId=" + roleId);

        var person = await _db.Persons
            .Include(p => p.RolesOfaPerson)
            .FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if already assigned
        if (person.RolesOfaPerson.Any(pp => pp.RoleId == roleId))
            return AssignmentStatus.LinkAlreadyExists;
 
        var role = await _db.Roles
            .Include(p => p.PersonsWithRole)
            .FirstOrDefaultAsync(p => p.RoleId == roleId);

        if (role == null)
            return AssignmentStatus.NoRoleExists;

        var personRole = new PersonRole
        {
            PersonId = personId,
            RoleId = roleId
        };

        person.RolesOfaPerson.Add(personRole);
        role.PersonsWithRole.Add(personRole);

        await _db.SaveChangesAsync();
        return AssignmentStatus.Success;
    }

    // Unassign an privilege from a person - called from Person Controller
    public async Task<AssignmentStatus> UnAssignPrivilegeFromPersonAsync(int personId, int privilegeId)
    {
        Debug.WriteLine("<<..debug..>> DbPersonRepository.cs/UnAssignPrivilegeFromPersonAsync:  personId=" + personId + "  privilegeId=" + privilegeId);

        var person = await _db.Persons
            .Include(p => p.PrivilegesOfaPerson)
            .FirstOrDefaultAsync(p => p.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if privilege exists
        var privilege = await _db.Privileges
            .Include(p => p.PersonsWithThisPrivilege)
            .FirstOrDefaultAsync(p => p.PrivilegeId == privilegeId);

        if (privilege == null)
            return AssignmentStatus.NoPrivilegeExists;

        // Delete person/privilege assignment link
        // Retrieve the PersonPrivilege object to be deleted using specific personId and privilegeId
        var personPrivilege = await _db.PersonPrivileges
            .Include(pp => pp.Person)      // Include related Person
            .Include(pp => pp.Privilege)   // Include related Privilege
            .FirstOrDefaultAsync(pp => pp.PersonId == personId && pp.PrivilegeId == privilegeId);

        //If PersonPrivilege object exists, remove it
        if (personPrivilege != null)
        {
            // Remove the personPrivilege from both navigation properties if they exist
            _db.PersonPrivileges.Remove(personPrivilege);
            if (personPrivilege.Person != null)
            {
                personPrivilege.Person.PrivilegesOfaPerson.Remove(personPrivilege);
            }
            if (personPrivilege.Privilege != null)
            {
                personPrivilege.Privilege.PersonsWithThisPrivilege.Remove(personPrivilege);
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

    // Unassign an skill from a person - called from Person Controller
    public async Task<AssignmentStatus> UnAssignSkillFromPersonAsync(int personId, int skillId)
    {
        Debug.WriteLine("<<..debug..>> DbPersonRepository.cs/UnAssignSkillFromPersonAsync:  personId=" + personId + "  skillId=" + skillId);

        var person = await _db.Persons
            .Include(r => r.SkillsOfaPerson)
            .FirstOrDefaultAsync(r => r.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if skill exists
        var skill = await _db.Skills
            .Include(s => s.PersonsWithThisSkill)
            .FirstOrDefaultAsync(s => s.SkillId == skillId);

        if (skill == null)
            return AssignmentStatus.NoSkillExists;

        // Delete person/skill assignment link
        // Retrieve the PersonSkill object to be deleted using specific personId and skillId
        var personSkill = await _db.PersonSkills
            .Include(ps => ps.Person)   // Include related Person
            .Include(ps => ps.Skill)    // Include related Skill
            .FirstOrDefaultAsync(ps => ps.PersonId == personId && ps.SkillId == skillId);

        //If PersonSkill object exists, remove it
        if (personSkill != null)
        {
            // Remove the personSkill from both navigation properties if they exist
            _db.PersonSkills.Remove(personSkill);
            if (personSkill.Person != null)
            {
                personSkill.Person.SkillsOfaPerson.Remove(personSkill);
            }
            if (personSkill.Skill != null)
            {
                personSkill.Skill.PersonsWithThisSkill.Remove(personSkill);
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

    // Unassign an role from a person - called from Person Controller
    public async Task<AssignmentStatus> UnAssignRoleFromPersonAsync(int personId, int roleId)
    {
        Debug.WriteLine("<<..debug..>> DbPersonRepository.cs/UnAssignRoleFromPersonAsync:  personId=" + personId + "  roleId=" + roleId);

        var person = await _db.Persons
            .Include(r => r.RolesOfaPerson)
            .FirstOrDefaultAsync(r => r.PersonId == personId);

        if (person == null)
            return AssignmentStatus.NoPersonExists;

        // Check if role exists
        var role = await _db.Roles
            .Include(r => r.PersonsWithRole)
            .FirstOrDefaultAsync(r => r.RoleId == roleId);

        if (role == null)
            return AssignmentStatus.NoRoleExists;

        // Delete person/role assignment link
        // Retrieve the PersonRole object to be deleted using specific personId and roleId
        var personRole = await _db.PersonRoles
            .Include(pr => pr.Person)   // Include related Person
            .Include(pr => pr.Role)     // Include related Role
            .FirstOrDefaultAsync(pr => pr.PersonId == personId && pr.RoleId == roleId);

        //If PersonRole object exists, remove it
        if (personRole != null)
        {
            // Remove the personRole from both navigation properties if they exist
            _db.PersonRoles.Remove(personRole);
            if (personRole.Person != null)
            {
                personRole.Person.RolesOfaPerson.Remove(personRole);
            }
            if (personRole.Role != null)
            {
                personRole.Role.PersonsWithRole.Remove(personRole);
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

    public async Task<Person> CreateAsync(Person person)
    {
        await _db.Persons.AddAsync(person);

        await _db.SaveChangesAsync();

        return person;

    }

    public async Task<Person> GetByIdAsync(int id)
    {
        return await _db.Persons.FindAsync(id);
    }

    //Read specific person by id and include collections for assigned (linked) privileges, skills and roles
    public async Task<Person> GetByIdDetailsAsync(int id)
    {
        return await _db.Persons
            .Include(p => p.PrivilegesOfaPerson)
                .ThenInclude(pp => pp.Privilege)
            .Include(p => p.SkillsOfaPerson)
                .ThenInclude(ps => ps.Skill)
            .Include(p => p.RolesOfaPerson)
                .ThenInclude(pr => pr.Role)
            .FirstOrDefaultAsync(p => p.PersonId == id);

    }

    //Update person
    public async Task<Person> UpdateAsync(Person person)
    {
        _db.Persons.Update(person);
        await _db.SaveChangesAsync();
        return person; //could be wrong
    }

    //Delete person
    public async Task DeleteAsync(int id)
    {
        Person? personToDelete = await GetByIdAsync(id);
        if (personToDelete != null)
        {
            _db.Persons.Remove(personToDelete);
            await _db.SaveChangesAsync();
        }
    }
}
