using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library.Repositories.Abstraction;
using PackagesExplorer.DataAccess;

namespace PackagesExplorer.Library.Repositories
{
    public class TopTrendingWriter : ITopTrendingWriter
    {
        private readonly IRepository<TrendingRepositories> repository;

        public TopTrendingWriter(IRepository<TrendingRepositories> repository)
        {
            this.repository = repository;
        }

        public async Task<ApiResponse> SaveAsync(Models.Inputs.TrendingRepositoriesInputDto trending, CancellationToken cancellationToken = default)
        {
            await this.repository.CreateAsync(new TrendingRepositories()
            {
                Date = DateTime.Now,
                Repositories = trending.Repositories.Select(r => new Repository()
                {
                    Uri = r,
                }).ToArray(),
            }, cancellationToken);

            await this.repository.SaveAsync(cancellationToken);

            return ApiResponse.Success();
        }
    }
}
