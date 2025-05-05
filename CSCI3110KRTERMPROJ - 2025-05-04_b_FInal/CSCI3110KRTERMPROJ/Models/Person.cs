using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public string FName { get; set; } = string.Empty;
        public string MName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = string.Empty;  // This should be unique in the database
        public string TypeCode { get; set; } = string.Empty;  //full-time, part-time, contractor, intern, etc.

        private char _userFlag;
        public char UserFlag
        {
            get => _userFlag;
            set => _userFlag = value == 'Y' || value == 'N' ? value : 'N';  // Enforce valid values
        }
        // Navigation property to link person and privileges using the association link class
        public ICollection<PersonPrivilege> PrivilegesOfaPerson { get; set; }
            = new List<PersonPrivilege>();

        // Navigation property to link person and skills using the association link class
        public ICollection<PersonSkill> SkillsOfaPerson { get; set; }
            = new List<PersonSkill>();

        // Navigation property to link person and roles using the association link class
        public ICollection<PersonRole> RolesOfaPerson { get; set; }
            = new List<PersonRole>();
    }


}
