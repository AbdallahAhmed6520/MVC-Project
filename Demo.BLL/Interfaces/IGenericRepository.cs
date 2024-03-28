using System.Collections.Generic;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(T item);
    }
}
