using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public interface ISolutionsStore
    {
        Task<IQueryable<SolutionDao>> GetSolutions(CancellationToken cancellationToken = default);

        Task CreateSolution(SolutionDao solution, CancellationToken cancellationToken = default);

        Task SaveInvalidSolution(InvalidSolutionDao solution, CancellationToken cancellationToken = default);

        Task<IQueryable<InvalidSolutionDao>> GetInvalidSolutions(CancellationToken cancellationToken = default);
    }
}
