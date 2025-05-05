using CSCI3110KRTERMPROJ.Models;

namespace CSCI3110KRTERMPROJ.Services
{
    public interface IPersonRoleRepository
    {
        Task<PersonRole> GetByIdAsync(int id);
        Task<PersonRole> GetByPersonIdAsync(int personid);
        Task<PersonRole> GetByRoleIdAsync(int roleid);
        Task DeleteAsync(int id);
    }
}
