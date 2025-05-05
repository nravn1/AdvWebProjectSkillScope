using CSCI3110KRTERMPROJ.Services;

namespace CSCI3110KRTERMPROJ.Services
{
    public class PersonSkillRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonSkillRepository(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
