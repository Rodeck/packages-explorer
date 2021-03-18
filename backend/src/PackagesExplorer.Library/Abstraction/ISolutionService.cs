using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.Models.Outputs;

namespace PackagesExplorer.Library.Abstraction
{
    public interface ISolutionService
    {
        Task<ApiResponse<IEnumerable<Solution>>> GetSolutions(string packageName, CancellationToken cancellationToken = default);

        Task<ApiResponse<IEnumerable<Solution>>> GetSolutions(string packageName, string version, CancellationToken cancellationToken = default);
    }
}
