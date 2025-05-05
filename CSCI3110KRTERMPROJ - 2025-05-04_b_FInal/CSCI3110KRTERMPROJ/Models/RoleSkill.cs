using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class RoleSkill
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Keys
        public int RoleId { get; set; }    //Foreign key
        public Role? Role { get; set; }    //Navigation property

        public int SkillId { get; set; }   //Foreign key
        public Skill? Skill { get; set; }  //Navigation property

        //public ICollection<RoleSkill> Skills { get; set; }
        //    = new List<RoleSkill>();

        //public ICollection<RoleSkill> Roles { get; set; }
        //    = new List<RoleSkill>();

        // Navigation properties for related entities (optional, if using ORM like EF Core)
        //[ForeignKey("RoleId")]
        //public virtual Role Role { get; set; }

        //[ForeignKey("SkillId")]
        //public virtual Skill Skill { get; set; }


    }


}
