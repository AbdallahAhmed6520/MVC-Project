using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
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
        public int Add(T item)
        {
            _dbContext.Add(item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            _dbContext.Remove(item);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            _dbContext.Update(item);
            return _dbContext.SaveChanges();
        }
    }
}
