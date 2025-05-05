using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface IPrivilegeRepository
    {
        Task<ICollection<Privilege>> ReadAllAsync();
        Task<Privilege> CreateAsync(Privilege privilege);
        Task<Privilege> GetByIdAsync(int privilegeid);
        Task<Privilege> GetByIdDetailsAsync(int privilegeid);
        Task<Privilege> UpdateAsync(Privilege privilege);
        Task DeleteAsync(int privilegeid);
        Task<Privilege?> ReadAsync(int id);
    }
}
