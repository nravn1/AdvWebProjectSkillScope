using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface IPersonRepository
    {
        Task<ICollection<Person>> ReadAllAsync();
        Task<Person> CreateAsync(Person person);
        Task<Person> GetByIdAsync(int personid);
        Task<Person> GetByIdDetailsAsync(int personid);
        Task<Person> UpdateAsync(Person person);
        Task DeleteAsync(int personid);
        Task<AssignmentStatus> AssignPrivilegeToPersonAsync(int personId, int privilegeId);
        Task<AssignmentStatus> AssignSkillToPersonAsync(int personId, int skillId);
        Task<AssignmentStatus> AssignRoleToPersonAsync(int personId, int roleId);
        Task<AssignmentStatus> UnAssignPrivilegeFromPersonAsync(int personId, int privilegeId);
        Task<AssignmentStatus> UnAssignSkillFromPersonAsync(int personId, int skillId);
        Task<AssignmentStatus> UnAssignRoleFromPersonAsync(int personId, int roleId);
        Task<Person?> ReadAsync(int id);
    }
}
