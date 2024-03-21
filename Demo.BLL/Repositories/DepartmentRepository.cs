using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly MVCAppContext _dbContext;
		public DepartmentRepository(MVCAppContext dbContext)
		{
			_dbContext = dbContext;
		}
		public int Add(Department department)
		{
			throw new NotImplementedException();
		}

		public int Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Department> GetAll()
		{
			throw new NotImplementedException();
		}

		public Department GetById(int id)
		{
			throw new NotImplementedException();
		}

		public int Update(Department department)
		{
			throw new NotImplementedException();
		}
	}
}
