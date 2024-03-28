using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCAppContext _dbContext;

        public GenericRepository(MVCAppContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T item)
        {
            _dbContext.Add(item);
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_dbContext.Employees.Include(E => E.Department).ToList();
            }
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}
