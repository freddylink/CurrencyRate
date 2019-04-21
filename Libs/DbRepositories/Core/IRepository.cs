using System;
using System.Collections.Generic;
using System.Text;

namespace DbRepositories.Core
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        void Add(TEntity entity);

        void Remove(TEntity entity);
    }
}
