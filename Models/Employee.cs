using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMAWS_T2204M_NguyenHoangLong.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Length must be 2 to 150")]
        public string EmployeeName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EmployeeDOB { get; set; }
        public string EmployeeDepartment { get; set; }
        public ICollection<ProjectEmployee> ProjectEmployees { get; set; }
        public Employee()
        {
            ProjectEmployees = new HashSet<ProjectEmployee>();
        }
    }
}
