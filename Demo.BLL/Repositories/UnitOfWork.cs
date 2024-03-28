using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
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
    }
}
