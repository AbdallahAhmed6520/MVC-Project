using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
	public class MVCAppContext : DbContext
	{
		public MVCAppContext(DbContextOptions<MVCAppContext> options) : base(options)
		{

		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.UseSqlServer("Server = .; Database = MVCApp; Trusted_Connection = true;");

		public DbSet<Department> Departments { get; set; }
	}
}
