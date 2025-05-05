using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models
{
    public class PersonRole
    {
        // Primary Key
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Foreign Keys
        public int PersonId { get; set; }

        public int RoleId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        // Navigation properties for related entities (optional, if using ORM like EF Core)
        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }

}
