using CSCI3110KRTERMPROJ.Services;

namespace CSCI3110KRTERMPROJ.Services
{
    public class PersonRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
