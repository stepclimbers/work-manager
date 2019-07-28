using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkManager.Data;
using WorkManager.Data.Models.Base;

namespace Repositories
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly WorkManagerDbContext db;

        public BaseRepository(WorkManagerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.db.Set<TEntity>();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await this.db.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Create(TEntity entity)
        {
            await this.db.Set<TEntity>().AddAsync(entity);
            await this.db.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            this.db.Set<TEntity>().Update(entity);
            await this.db.SaveChangesAsync();
        }

        public async Task Delete(TKey id)
        {
            var entity = await this.GetById(id);
            if (entity != null)
            {
                this.db.Set<TEntity>().Remove(entity);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
