using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();

        Task SaveAsync(CancellationToken cancellationToken = default);

        void Update(TEntity entity);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        void DeleteAsync(TEntity entity);
    }
}
