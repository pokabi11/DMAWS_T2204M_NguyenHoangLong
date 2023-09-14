using Microsoft.EntityFrameworkCore;

namespace DMAWS_T2204M_NguyenHoangLong.Models
{
    public class Context : DbContext
    {
		public Context(DbContextOptions options): base(options)
		{
		}

        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
