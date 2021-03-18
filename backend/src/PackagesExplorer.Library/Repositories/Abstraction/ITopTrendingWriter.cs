using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.Models.Inputs;

namespace PackagesExplorer.Library.Repositories.Abstraction
{
    public interface ITopTrendingWriter
    {
        public Task<ApiResponse> SaveAsync(TrendingRepositories trending, CancellationToken cancellationToken = default);
    }
}
