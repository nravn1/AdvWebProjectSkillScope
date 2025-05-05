using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface IRoleRepository
    {
        Task<Role?> ReadAsync(int roleid);
        Task<ICollection<Role>> ReadAllAsync();
        Task<ICollection<Role>> ReadAllForPersonAsync();
        Task<Role> CreateAsync(Role role);
        Task<Role> GetByIdAsync(int roleid);
        Task<Role> GetByIdDetailsAsync(int roleid);
        Task<Role> UpdateAsync(Role role);
        Task DeleteAsync(int roleid);
        Task<AssignmentStatus> AssignSkillToRoleAsync(int roleId, int skillId);
        Task<AssignmentStatus> UnAssignSkillFromRoleAsync(int roleId, int skillId);
    }
}
