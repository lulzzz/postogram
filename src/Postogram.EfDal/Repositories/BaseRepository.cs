using System;
using Microsoft.EntityFrameworkCore;
using Postogram.DataAccessLayer;

namespace Postogram.EfDal.Repositories
{
    public abstract class BaseRepository<TEntry, TKey> : IRepository<TEntry, TKey> where TEntry : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntry> _set;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntry>();
        }

        public virtual void Create(TEntry entry)
        {
            _set.Add(entry);
        }

        public virtual void Delete(TKey id)
        {
            var entry = Get(id);

            if(entry == null)
            {
                throw new ArgumentException($"Entry [{typeof(TEntry).Name}] with id='{id}' was not found");
            }

            _set.Remove(entry);
        }

        public virtual TEntry Get(TKey id)
        {
            return _set.Find(id);
        }

        public virtual void Update(TEntry entry)
        {
            _context.Entry(entry).State = EntityState.Modified;
        }
    }
}
