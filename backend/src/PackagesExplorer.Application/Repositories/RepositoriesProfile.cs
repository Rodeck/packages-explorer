using System.Linq;
using AutoMapper;

using PackagesExplorer.Application.Models.Outputs;
using PackagesExplorer.DataAccess;

namespace PackagesExplorer.Library.Repositories
{
    public class RepositoriesProfile : Profile
    {
        public RepositoriesProfile()
        {
            this.CreateMap<TrendingRepositories, TrendingRepositoriesOutputDto>()
                .ConstructUsing(r => Construct(r));
        }

        private static TrendingRepositoriesOutputDto Construct(TrendingRepositories trending)
        {
            var output = new TrendingRepositoriesOutputDto()
            {
                Date = trending.Date,
                Repositories = trending.Repositories.Select(rp => rp.Uri)
            };

            return output;
        }
    }
}
