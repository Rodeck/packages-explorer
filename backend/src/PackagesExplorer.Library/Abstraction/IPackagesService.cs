using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.Models.Inputs;

namespace PackagesExplorer.Library.Abstraction
{
    public interface IPackagesService
    {
        Task<ApiResponse<Solution>> CreateSolution(Solution solution, CancellationToken token = default);

        Task<ApiResponse<IEnumerable<Models.Outputs.InvalidSolution>>> GetInvalidSolutions(CancellationToken token = default);
    }
}
