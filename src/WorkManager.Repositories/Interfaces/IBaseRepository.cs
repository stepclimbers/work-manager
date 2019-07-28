using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetById(TKey id);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TKey id);
    }
}
