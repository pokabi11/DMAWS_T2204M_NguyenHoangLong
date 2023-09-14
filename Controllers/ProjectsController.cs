﻿using DMAWS_T2204M_NguyenHoangLong.Models;
using DMAWS_T2204M_NguyenHoangLong.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DMAWS_T2204M_NguyenHoangLong.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly Context _context;
        public ProjectsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        async public Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                var projects = _context.Projects.ToListAsync();
                return Ok(projects);
            }
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound() ;
            return Ok(project);
        }

        [HttpGet,Route("get-detail")]
        async public Task<IActionResult> GetDetail(int? id)
        {
            if (id == null)
            {
                var projects = _context.Projects.Include(e=>e.ProjectEmployees).ThenInclude(e=>e.EmployeeDOB).ToListAsync();
                return Ok(projects);
            }
            var project = await _context.Projects.Include(e => e.ProjectEmployees).ThenInclude(e => e.EmployeeDOB).Where(e=>e.ProjectId.Equals(id)).ToListAsync();
            if (project == null) return NotFound();
            return Ok(project);
        }

        [HttpGet,Route("search-by-name")]
        async public Task<IActionResult> SearchByName(string? name)
        {
            var p = await _context.Projects.Where(e=>e.ProjectName.Equals(name)).ToListAsync();
            return Ok(p);
        }
        [HttpGet,Route("search-by-startdate")]
        async public Task<IActionResult> SearchByStartDate(DateTime startdate)
        {
            if (startdate != null)
            {
                var ps = await _context.Projects.Where(e => e.ProjectStartDate.CompareTo(startdate)==0).ToListAsync();
                return Ok(ps);
            }

            var p = await _context.Projects.Where(e => e.ProjectStartDate.CompareTo(DateTime.Now) > 0).ToListAsync();
            return Ok(p);
        }

        [HttpGet, Route("search-by-enddate")]
        async public Task<IActionResult> SearchByEndDate( DateTime enddate)
        {
            if (enddate != null)
            {
                var ps = await _context.Projects.Where(e => e.ProjectEndDate.CompareTo(enddate) == 0).ToListAsync();
                return Ok(ps);
            }

            var p = await _context.Projects.Where(e => e.ProjectEndDate.CompareTo(DateTime.Now) < 0).ToListAsync();
            return Ok(p);
        }

        [HttpPost]
        async public Task<IActionResult> Create(ProjectDTO data)
        {
            if (ModelState.IsValid)
            {
                var project = new Project {ProjectName=data.ProjectName,ProjectEndDate=data.ProjectEndDate,ProjectStartDate=data.ProjectStartDate };
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
                return Ok(project);
            }
            return BadRequest();
        }

        [HttpPut]
        async public Task<IActionResult> Update (Project data)
        {
            if (ModelState.IsValid) {
                 _context.Projects.Update(data);
                await _context.SaveChangesAsync();
                return Ok(new {status=1});
            }
            return BadRequest();
        }
        [HttpDelete]
        async public Task<IActionResult> Delete (int id)
        {
            if (id != null)
            {
                var p = await _context.Projects.FindAsync(id);
                if (p == null) return NotFound();
                _context.Projects.Remove(p);
                await _context.SaveChangesAsync();
            }
            return BadRequest();
        }

    }
}