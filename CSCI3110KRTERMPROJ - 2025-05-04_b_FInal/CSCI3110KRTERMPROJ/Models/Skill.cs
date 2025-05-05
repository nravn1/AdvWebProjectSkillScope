using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class Skill
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }

        // Other columns
        [Required]
        [StringLength(100)]
        public string SkillTitle { get; set; } = string.Empty;

        // Assuming you're using a modern database system that supports NVARCHAR(MAX) or similar for large text fields instead of TEXT.
        public string Description { get; set; } = string.Empty;

        // Navigation property to link roles and skills using the association link class
        public ICollection<RoleSkill> RolesWithThisSkill { get; set; }
            = new List<RoleSkill>();

        // Navigation property to link persons and skills using the association link class
        public ICollection<PersonSkill> PersonsWithThisSkill { get; set; }
            = new List<PersonSkill>();
    }

}
