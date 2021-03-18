using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.Library.Abstraction
{
    public interface ISolutionValidator
    {
        public Task<bool> Validate(SolutionInputDto solution, out InvalidSolutionDao invalidSolutionModel, CancellationToken token = default);
    }
}
