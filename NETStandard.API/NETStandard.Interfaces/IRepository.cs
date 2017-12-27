using System.Collections.Generic;

namespace NETStandard.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T t);
        int Update(T t);
        int Delete(int id);
    }
}
