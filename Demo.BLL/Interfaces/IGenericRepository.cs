using System.Collections.Generic;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T item);
        int Update(T item);
        int Delete(T item);
    }
}
