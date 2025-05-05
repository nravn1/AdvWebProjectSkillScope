using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class PersonSkill
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Keys
        public int PersonId { get; set; }

        public int SkillId { get; set; }

        // Navigation properties for related entities (optional, if using ORM like EF Core)
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("SkillId")]
        public virtual Skill Skill { get; set; }
    }

}
