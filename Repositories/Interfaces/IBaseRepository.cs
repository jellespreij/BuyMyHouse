using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBaseRepository<T> where T : class, IEntityBase, new()
    {
        IEnumerable<T> GetAll();
        T GetSingle(Guid id);
        Task<T> Add(T entity);
        void Commit();
    }
}
