using CSCI3110KRTERMPROJ.Services;

namespace CSCI3110KRTERMPROJ.Services
{
    public class PersonPrivilegeRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonPrivilegeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
