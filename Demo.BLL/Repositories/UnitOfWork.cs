using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MVCAppContext _dbcontext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public UnitOfWork(MVCAppContext dbcontext)
        {
            EmployeeRepository = new EmployeeRepository(dbcontext);
            DepartmentRepository = new DepartmentRepository(dbcontext);
            _dbcontext = dbcontext;
        }

        public int Complete()
        {
            return _dbcontext.SaveChanges();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
