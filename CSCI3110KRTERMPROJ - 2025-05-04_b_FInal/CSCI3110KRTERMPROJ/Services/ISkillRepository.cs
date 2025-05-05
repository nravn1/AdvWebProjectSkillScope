using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface ISkillRepository
    {
        Task<Skill?> ReadAsync(int skillid);
        Task<ICollection<Skill>> ReadAllAsync();
        Task<ICollection<Skill>> ReadAllForRoleAsync();
        Task<ICollection<Skill>> ReadAllForPersonAsync();
        Task<Skill> CreateAsync(Skill skill);
        Task<Skill> GetByIdAsync(int skillid);
        Task<Skill> GetByIdDetailsAsync(int skillid);
        Task<Skill> UpdateAsync(Skill skill);
        Task DeleteAsync(int skillid);
    }
}
