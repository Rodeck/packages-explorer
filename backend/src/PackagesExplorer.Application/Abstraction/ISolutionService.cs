using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PackagesExplorer.Application.Models.Outputs;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library;

namespace PackagesExplorer.Application.Abstraction
{
    public interface ISolutionService
    {
        Task<ApiResponse<SolutionOutputDto>> CreateSolution(SolutionInputDto solution, CancellationToken token = default);

        Task<ApiResponse<IEnumerable<SolutionOutputDto>>> GetSolutions(string packageName, CancellationToken cancellationToken = default);

        Task<ApiResponse<IEnumerable<SolutionOutputDto>>> GetSolutions(string packageName, string version, CancellationToken cancellationToken = default);
    }
}
