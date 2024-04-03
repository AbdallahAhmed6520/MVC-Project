using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Demo.DAL.Contexts
{
	public class MVCAppContext : IdentityDbContext<ApplicationUser>
	{
		public MVCAppContext(DbContextOptions<MVCAppContext> options) : base(options)
		{

		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.UseSqlServer("Server = .; Database = MVCApp; Trusted_Connection = true;");

		public DbSet<Department> Departments { get; set; }
		public DbSet<Employee> Employees { get; set; }
	}
}
