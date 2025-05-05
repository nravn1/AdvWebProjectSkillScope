using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class PersonPrivilege
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Keys
        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int PrivilegeId { get; set; }
        public Privilege? Privilege { get; set; }

        // Navigation properties for related entities (optional, if using ORM like EF Core)
        //[ForeignKey("PersonId")]
        //public virtual Person Person { get; set; }

        //[ForeignKey("PrivilegeId")]
        //public virtual Privilege Privilege { get; set; }
    }

}
