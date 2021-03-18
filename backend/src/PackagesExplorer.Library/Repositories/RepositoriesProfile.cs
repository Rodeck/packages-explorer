using System.Linq;
using AutoMapper;
using PackagesExplorer.DataAccess;

namespace PackagesExplorer.Library.Repositories
{
    public class RepositoriesProfile : Profile
    {
        public RepositoriesProfile()
        {
            this.CreateMap<TrendingRepositories, Models.Outputs.TrendingRepositories>()
                .ConstructUsing(r => Construct(r));
        }

        private static Models.Outputs.TrendingRepositories Construct(TrendingRepositories trending)
        {
            var output = new Models.Outputs.TrendingRepositories()
            {
                Date = trending.Date,
                Repositories = trending.Repositories.Select(rp => rp.Uri)
            };

            return output;
        }
    }
}
