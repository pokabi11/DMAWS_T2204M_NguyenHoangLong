using DMAWS_T2204M_NguyenHoangLong.DTOs;
using DMAWS_T2204M_NguyenHoangLong.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2204M_NguyenHoangLong.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly Context _context;
        public EmployeeController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        async public Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                var es = _context.Employees.ToListAsync();
                return Ok(es);
            }
            var e = await _context.Employees.FindAsync(id);
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpGet, Route("get-detail")]
        async public Task<IActionResult> GetDetail(int? id)
        {
            if (id == null)
            {
                var es = _context.Employees.Include(e => e.ProjectEmployees).ThenInclude(e => e.Projects).ToListAsync();
                return Ok(es);
            }
            var e = await _context.Employees.Include(e => e.ProjectEmployees).ThenInclude(e => e.Projects).Where(e => e.EmployeeId.Equals(id)).ToListAsync();
            if (e == null) return NotFound();
            return Ok(e);
        }

        [HttpGet, Route("search-by-name")]
        async public Task<IActionResult> SearchByName(string? name)
        {
            var p = await _context.Employees.Where(e => e.EmployeeName.Equals(name)).ToListAsync();
            return Ok(p);
        }

        [HttpGet, Route("search-by-date")]
        async public Task<IActionResult> SearchByDate(DateTime from, DateTime to)
        {
            var p = await _context.Employees.Where(e => e.EmployeeDOB.CompareTo(from) > 0 && e.EmployeeDOB.CompareTo(to) < 0).ToListAsync();
            return Ok(p);
        }

        [HttpPost]
        async public Task<IActionResult> Create(EmployeeDTO data)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee { EmployeeName = data.EmployeeName, EmployeeDepartment = data.EmployeeDepartment, EmployeeDOB = data.EmployeeDOB };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            return BadRequest();
        }

        [HttpPut]
        async public Task<IActionResult> Update(Employee data)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(data);
                await _context.SaveChangesAsync();
                return Ok(new { status = 1 });
            }
            return BadRequest();
        }
        [HttpDelete]
        async public Task<IActionResult> Delete(int id)
        {
            if (id != null)
            {
                var e = await _context.Employees.FindAsync(id);
                if (e == null) return NotFound();
                _context.Employees.Remove(e);
                await _context.SaveChangesAsync();
            }
            return BadRequest();
        }
    }

}

