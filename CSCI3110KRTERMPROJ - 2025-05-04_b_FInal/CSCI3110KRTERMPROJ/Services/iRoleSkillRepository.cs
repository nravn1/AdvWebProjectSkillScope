using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface IRoleSkillRepository
    {
        Task<RoleSkill?> ReadAsync(int id);
        Task<ICollection<RoleSkill>> ReadAllAsync();
        Task<RoleSkill> CreateAsync(RoleSkill roleskill);
        Task<RoleSkill> GetByIdAsync(int id);
        Task<RoleSkill> UpdateAsync(RoleSkill roleskill);
        Task DeleteAsync(int id);
    }
}
