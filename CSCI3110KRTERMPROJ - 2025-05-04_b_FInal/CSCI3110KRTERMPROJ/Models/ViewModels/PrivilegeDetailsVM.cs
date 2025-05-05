using System.ComponentModel.DataAnnotations;

namespace CSCI3110KRTERMPROJ.Models.ViewModels
{
    public class PrivilegeDetailsVM
    {
        public int PrivilegeId { get; set; }
        public string? PrivilegeTitle { get; set; }
        public string? Description { get; set; }
        public int PersonCount { get; set; }
    }
}
