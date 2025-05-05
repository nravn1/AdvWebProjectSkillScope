//using CSCI3110KRTERMPROJ.Models;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models.ViewModels
{
    public class SkillDetailsVM
    {
        public int SkillId { get; set; }
        public string? SkillTitle { get; set; }
        public string? Description { get; set; }
        public int SkillRoleCount { get; set; }
        public int SkillPersonCount { get; set; }
    }
}
