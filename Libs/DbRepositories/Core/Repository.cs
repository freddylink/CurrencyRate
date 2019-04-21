using Microsoft.EntityFrameworkCore;

namespace DbRepositories.Core
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

        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
            SaveChanges();
        }

        protected void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
