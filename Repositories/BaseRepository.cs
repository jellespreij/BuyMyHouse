using Interfaces;
using Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;

namespace Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        protected CosmosDBContext _context;

        public BaseRepository(CosmosDBContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
            await _context.Database.EnsureCreatedAsync();
            _context.Set<T>().Add(entity);

            Commit();

            return entity;
        }

        public async void Commit()
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetSingle(Guid id)
        {
            return _context.Set<T>().Where(entity => entity.Id == id).FirstOrDefault();
        }
    }
}
