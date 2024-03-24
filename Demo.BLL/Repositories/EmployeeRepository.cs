using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System.Linq;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppContext _dbContext;

        public EmployeeRepository(MVCAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return _dbContext.Employees.Where(E => E.Address == address);
        }

        //private readonly MVCAppContext _dbContext;
        //public EmployeeRepository(MVCAppContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}
        //public int Add(Employee employee)
        //{
        //    _dbContext.Add(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _dbContext.Remove(employee);
        //    return _dbContext.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //    => _dbContext.Employees.ToList();

        //public Employee GetById(int id)
        //{
        //    //var Employee = _dbContext.Employees.Local.Where(d => d.Id == id).FirstOrDefault();
        //    //if (Employee is null)
        //    //	Employee = _dbContext.Employees.Where(d => d.Id == id).FirstOrDefault();
        //    //return Employee;
        //    return _dbContext.Employees.Find(id);
        //}

        //public int Update(Employee employee)
        //{
        //    _dbContext.Update(employee);
        //    return _dbContext.SaveChanges();
        //}
    }
}
