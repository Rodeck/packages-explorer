using Microsoft.EntityFrameworkCore;

namespace PackagesExplorer.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<TrendingRepositories> TrendingRepositories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }
    }
}
