using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWS_T2204M_NguyenHoangLong.Models
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Length must be 2 to 150")]
        public string ProjectName { get; set; }
        [Required]
        public DateTime ProjectStartDate { get; set; }
        [Required]
        public DateTime? ProjectEndDate { get; set; }
        public ICollection<Employee> ProjectEmployees { get; set; }
        
    }
}
