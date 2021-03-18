using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using PackagesExplorer.Application.Models.Outputs;
using PackagesExplorer.DataAccess;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library.Repositories.Abstraction;

namespace PackagesExplorer.Library.Repositories
{
    public class TopTrendingReader : ITopTrendingReader
    {
        private readonly IRepository<TrendingRepositories> repository;
        private readonly IMapper mapper;

        public TopTrendingReader(IRepository<TrendingRepositories> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ApiCollectionResponse<TrendingRepositoriesOutputDto>> GetTopTrendingAsync(CancellationToken cancellationToken = default)
        {
            var repositories = this.repository.GetAll();

            return this.BuildResponse(repositories);
        }

        public async Task<ApiCollectionResponse<TrendingRepositoriesOutputDto>> GetTopTrendingAsync(DateTime day, CancellationToken cancellationToken = default)
        {
            var repositories = this.repository.GetAll()
                .Where(r => r.Date.Date == day.Date);

            return this.BuildResponse(repositories);
        }

        public async Task<ApiCollectionResponse<TrendingRepositoriesOutputDto>> GetTopTrendingAsync(DateTime @from, DateTime to, CancellationToken cancellationToken = default)
        {
            var repositories = this.repository.GetAll()
                .Where(r => r.Date.Date >= @from.Date && r.Date.Date < to.Date);

            return this.BuildResponse(repositories);
        }

        private ApiCollectionResponse<TrendingRepositoriesOutputDto> BuildResponse(IQueryable<TrendingRepositories> repositories)
        {
            var mapped = repositories.Select(r => new TrendingRepositoriesOutputDto()
            {
                Date = r.Date,
                Repositories = r.Repositories.Select(rp => rp.Uri),
            });

            return ApiCollectionResponse<TrendingRepositoriesOutputDto>.Success(mapped);
        }
    }
}
