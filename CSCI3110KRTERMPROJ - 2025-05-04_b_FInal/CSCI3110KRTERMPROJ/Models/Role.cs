using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class Role
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        // Other columns
        [Required]
        [StringLength(100)]
        public string RoleTitle { get; set; } = string.Empty;

        // Assuming you're using a modern database system that supports NVARCHAR(MAX) or similar for large text fields instead of TEXT.
        public string Description { get; set; } = string.Empty;

        // Navigation property to link roles and skills using the association link class
        public ICollection<RoleSkill> SkillsOfaRole { get; set; }
            = new List<RoleSkill>();
        public ICollection<PersonRole> PersonsWithRole { get; set; }
            = new List<PersonRole>();
        //public ICollection<RoleSkill> Skills { get; set; }
        //    = new List<RoleSkill>();

    }

}
