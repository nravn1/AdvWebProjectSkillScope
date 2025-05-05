using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models.ViewModels
{
    public class PersonDetailsVM
    {
        public int PersonId { get; set; }
        public string? FName { get; set; }
        public string? MName { get; set; }
        public string? LName { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? TypeCode { get; set; }
        public char? UserFlag { get; set; }
        public int PrivilegeCount { get; set; }
        public int SkillCount { get; set; }
        public int RoleCount { get; set; }
    }
}
