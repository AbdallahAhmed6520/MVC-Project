using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetById(int id);
        public int Add(Employee employee);
        public int Update(Employee employee);
        public int Delete(Employee employee);
    }
}
