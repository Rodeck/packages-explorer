using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PackagesExplorer.Application.Dao;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public class InMemorySolutionsStore : ISolutionsStore
    {
        private readonly IStore store;

        public InMemorySolutionsStore(IStore store)
        {
            this.store = store;
        }

        public async Task<IQueryable<SolutionInputDto>> GetSolutions(CancellationToken cancellationToken = default)
        {
            var solutions = await this.store.GetSolutions(cancellationToken);
            return solutions.AsQueryable();
        }

        public Task CreateSolution(SolutionInputDto solution, CancellationToken cancellationToken = default)
        {
            return this.store.CreateSolution(solution, cancellationToken);
        }

        public Task SaveInvalidSolution(InvalidSolutionDao solution, CancellationToken cancellationToken = default)
        {
            return this.store.CreateInvalidSolution(solution, cancellationToken);
        }

        public async Task<IQueryable<InvalidSolutionDao>> GetInvalidSolutions(CancellationToken cancellationToken = default)
        {
            var solutions = await this.store.GetInvalidSolutions(cancellationToken);
            return solutions.AsQueryable();
        }
    }
}
