using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using PackagesExplorer.Application.Models.Outputs;
using PackagesExplorer.Library;

namespace PackagesExplorer.Application.Abstraction
{
    interface IInvalidSolutionService
    {
        Task<ApiResponse<IEnumerable<InvalidSolutionOutputDto>>> GetInvalidSolutions(CancellationToken token = default);
    }
}
