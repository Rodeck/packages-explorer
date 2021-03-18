using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.DataAccess
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        protected readonly ApplicationContext Context;

        protected BaseRepository(ApplicationContext context)
        {
            this.Context = context;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return this.Context.Set<TEntity>();
        }

        public Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return this.Context.SaveChangesAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            this.Context.Update<TEntity>(entity);
        }

        public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.Context.AddAsync(entity, cancellationToken);
        }

        public void DeleteAsync(TEntity entity)
        {
            this.Context.Remove(entity);
        }
    }
}
