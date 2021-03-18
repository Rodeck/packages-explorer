using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PackagesExplorer.DataAccess.Repositories
{
    public class TrendingRepositoriesRepository : BaseRepository<TrendingRepositories>
    {
        public TrendingRepositoriesRepository(ApplicationContext context) : base(context)
        {

        }

        public override IQueryable<TrendingRepositories> GetAll()
        {
            return this.Context.Set<TrendingRepositories>()
                .Include(r => r.Repositories);
        }
    }
}
