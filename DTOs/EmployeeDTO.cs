namespace DMAWS_T2204M_NguyenHoangLong.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime EmployeeDOB { get; set; }
        public string EmployeeDepartment { get; set; }
        public List<EmployeeDTO> EmployeeDTOs { get; set; }
    }
}
