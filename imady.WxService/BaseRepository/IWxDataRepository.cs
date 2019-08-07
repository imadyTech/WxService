using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace imady.Repository
{
    public interface IWxDataRepository <T>
    {

        T Get(Guid id);

        IEnumerable<T> GetAll();

        void Create(T model);

        void Update(T model);

        void Delete(Guid id);

    }
}
