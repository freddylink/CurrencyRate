using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DbRepositories.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class
    {
        readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            Entities = context.Set<TEntity>();
        }

        protected DbSet<TEntity> Entities { get; }


        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            SaveChanges();
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            SaveChanges();
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
