using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110KRTERMPROJ.Controllers
{
    //Send a GET request to localhost:portnumber/api/skills
    [Route("api/skills")]
    [ApiController]
    public class SkillsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SkillsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllSkills()
        {
            var skills = _context.Skills.ToList();
            return Ok(skills); // returns JSON
        }
    }
}
