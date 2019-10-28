using System.Collections.Generic;

namespace DbRepositories.Data
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);
    }
}
