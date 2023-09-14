using DMAWS_T2204M_NguyenHoangLong.DTOs;
using DMAWS_T2204M_NguyenHoangLong.Models;
using Microsoft.AspNetCore.Mvc;

namespace DMAWS_T2204M_NguyenHoangLong.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeesController : Controller
    {
        private readonly DataContext _context;

        public EmployeesController(DataContext dataContext)
        {
            _context = dataContext;
        }
        //GET Employee
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _context.Employees.ToList;
            List<EmployeeDTO> list = new List<EmployeeDTO>();
            return Ok(list);
        }
        [HttpGet]
        [Route("get-by-id")]
    

    }
}
