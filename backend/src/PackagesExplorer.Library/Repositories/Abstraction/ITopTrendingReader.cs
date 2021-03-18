using System;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.Models.Outputs;

namespace PackagesExplorer.Library.Repositories.Abstraction
{
    public interface ITopTrendingReader
    {
        public Task<ApiCollectionResponse<TrendingRepositories>> GetTopTrendingAsync(CancellationToken cancellationToken = default);
        public Task<ApiCollectionResponse<TrendingRepositories>> GetTopTrendingAsync(DateTime day, CancellationToken cancellationToken = default);
        public Task<ApiCollectionResponse<TrendingRepositories>> GetTopTrendingAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}
