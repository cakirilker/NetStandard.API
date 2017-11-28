using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETStandard.Shared
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T t);
        int Update(T t);
        int Delete(int id);
        IEnumerable<T> FindBy(Func<T, bool> predicate);
    }
}
