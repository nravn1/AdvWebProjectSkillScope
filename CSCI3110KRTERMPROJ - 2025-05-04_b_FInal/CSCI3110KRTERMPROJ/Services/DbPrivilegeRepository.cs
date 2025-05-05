using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110KRTERMPROJ.Services;

public class DbPrivilegeRepository : IPrivilegeRepository
{
    private readonly ApplicationDbContext _db;

    public DbPrivilegeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    //Reads a specific Privilege and loads collection of persons
    public async Task<ICollection<Privilege>> ReadAllAsync()
    {
        return await _db.Privileges
            .Include(p => p.PersonsWithThisPrivilege)
            .ToListAsync();
    }

    // Get a specific privilege with its related persons
    public async Task<Privilege?> ReadAsync(int id)
    {
        return await _db.Privileges
            .Include(p => p.PersonsWithThisPrivilege)
            .ThenInclude(pp => pp.Person)
            .FirstOrDefaultAsync(p => p.PrivilegeId == id);
    }

    //Create privilege
    public async Task<Privilege> CreateAsync(Privilege privilege)
    {
        await _db.Privileges.AddAsync(privilege);

        await _db.SaveChangesAsync();

        return privilege;

    }

    public async Task<Privilege> GetByIdAsync(int id)
    {
        return await _db.Privileges.FindAsync(id);
    }

    //Reads a specific Privilege and loads collection of persons
    public async Task<Privilege> GetByIdDetailsAsync(int id)
    {
        return await _db.Privileges
            .Include(p => p.PersonsWithThisPrivilege)
            .ThenInclude(pp => pp.Person)
            .FirstOrDefaultAsync(p => p.PrivilegeId == id);
    }

    //Update privilege
    public async Task<Privilege> UpdateAsync(Privilege privilege)
    {
        _db.Privileges.Update(privilege);
        await _db.SaveChangesAsync();
        return privilege; //could be wrong
    }

    //Delete privilege
    public async Task DeleteAsync(int id)
    {
        Privilege? privilegeToDelete = await GetByIdAsync(id);
        if (privilegeToDelete != null)
        {
            _db.Privileges.Remove(privilegeToDelete);
            await _db.SaveChangesAsync();
        }
    }
}
