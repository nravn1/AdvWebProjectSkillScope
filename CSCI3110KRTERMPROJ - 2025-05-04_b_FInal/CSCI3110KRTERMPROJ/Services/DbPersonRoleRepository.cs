using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CSCI3110KRTERMPROJ.Services;

public class DbPersonRoleRepository : IPersonRoleRepository
{
    private readonly ApplicationDbContext _db;

    public DbPersonRoleRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<PersonRole> GetByIdAsync(int id)
    {
        return await _db.PersonRoles.FindAsync(id);
    }

    //Get the personRole links for a specific personId (looking for roles linked to a specific person)
    public async Task<PersonRole> GetByPersonIdAsync(int id)
    {
        return await _db.PersonRoles
            .FirstOrDefaultAsync(p => p.PersonId == id);

    }

    //Get the personRole links for a specific roleId (looking for persons linked to a specific role))
    public async Task<PersonRole> GetByRoleIdAsync(int id)
    {
        return await _db.PersonRoles
            .FirstOrDefaultAsync(r => r.RoleId == id);

    }

    public async Task DeleteAsync(int id)
    {
        PersonRole? personRoleToDelete = await GetByIdAsync(id);
        if (personRoleToDelete != null)
        {
            _db.PersonRoles.Remove(personRoleToDelete);
            await _db.SaveChangesAsync();
        }
        
            
        
    }
}
