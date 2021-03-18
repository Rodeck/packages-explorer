using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.DataAccess
{
    public class InMemoryStore: IStore
    {
        private readonly IDictionary<string, SolutionDao> solutions = new Dictionary<string, SolutionDao>();
        private readonly IDictionary<(string, DateTime), InvalidSolutionDao> invalidSolutions = new Dictionary<(string, DateTime), InvalidSolutionDao>();

        public Task CreateSolution(SolutionDao solution, CancellationToken cancellationToken = default)
        {
            solutions[solution.Url] = solution;

            return Task.CompletedTask;
        }

        public Task<IEnumerable<SolutionDao>> GetSolutions(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(solutions.Values.AsEnumerable());
        }

        public Task CreateInvalidSolution(InvalidSolutionDao solution, CancellationToken cancellationToken = default)
        {
            invalidSolutions[(solution.Uri, solution.ScrappingDate)] = solution;

            return Task.CompletedTask;
        }

        public Task<IEnumerable<InvalidSolutionDao>> GetInvalidSolutions(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(invalidSolutions.Values.AsEnumerable());
        }
    }
}
