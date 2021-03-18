using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public interface IStore
    {
        public Task CreateSolution(SolutionDao solution, CancellationToken cancellationToken = default);

        public Task<IEnumerable<SolutionDao>> GetSolutions(CancellationToken cancellationToken = default);
        
        public Task CreateInvalidSolution(InvalidSolutionDao solution, CancellationToken cancellationToken = default);

        public Task<IEnumerable<InvalidSolutionDao>> GetInvalidSolutions(CancellationToken cancellationToken = default);
    }
}
