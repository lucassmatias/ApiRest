using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Abstractions
{
    public interface ICRUD<T>
    {
        T Save(T entity);
        IList<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
