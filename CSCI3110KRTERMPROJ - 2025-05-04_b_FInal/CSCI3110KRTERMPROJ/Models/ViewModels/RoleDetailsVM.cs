using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models.ViewModels
{
    public class RoleDetailsVM
    {
        public int RoleId { get; set; }
        public string? RoleTitle { get; set; }
        public string? Description { get; set; }
        public int RoleSkillCount { get; set; }
        public int RolePersonCount { get; set; }
    }
}
