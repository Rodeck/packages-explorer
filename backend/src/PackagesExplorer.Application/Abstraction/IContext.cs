using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackagesExplorer.Application.Abstraction
{
    public interface IContext<TEntity>
    {
        public IQueryable<TEntity> Entities { get; }

        public Task CreateAsync(TEntity solution, CancellationToken cancellationToken = default);

        public Task UpdateAsync(TEntity solution, CancellationToken cancellationToken = default);

        public Task DeleteAsync(TEntity solution, CancellationToken cancellationToken = default);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
