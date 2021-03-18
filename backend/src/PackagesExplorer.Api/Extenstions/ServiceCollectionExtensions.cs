using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PackagesExplorer.DataAccess;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.DataAccess.Repositories;
using PackagesExplorer.Library;
using PackagesExplorer.Library.Abstraction;
using PackagesExplorer.Library.Options;
using PackagesExplorer.Library.Repositories;
using PackagesExplorer.Library.Repositories.Abstraction;

namespace PackagesExplorer.Api.Extenstions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExplorerServices(this IServiceCollection services)
        {
            return services.AddSingleton<ISolutionService, SolutionService>()
                .AddSingleton<IPackagesService, PackagesService>()
                .AddDefaultServices();
        }

        public static IServiceCollection AddInMemoryExplorerServices(this IServiceCollection services)
        {
            return services.AddSingleton<ISolutionService, SolutionService>()
                .AddSingleton<IPackagesService, PackagesService>()
                .AddSingleton<IStore, InMemoryStore>()
                .AddSingleton<ISolutionsStore, InMemorySolutionsStore>()
                .AddDefaultServices();
        }
        public static IServiceCollection AddJsonExplorerServices(this IServiceCollection services)
        {
            return services.AddScoped<ISolutionService, SolutionService>()
                .AddScoped<IPackagesService, PackagesService>()
                .AddScoped<IStore, JsonFileStore>()
                .AddScoped<ISolutionsStore, InMemorySolutionsStore>()
                .AddDefaultServices();
        }
        
        private static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            return services.AddScoped<ISolutionValidator, SolutionValidator>()
                .Configure<SolutionValidatorOptions>(opt => opt.FailedPackagesThreshold = 0.5f)
                .AddScoped<ITopTrendingWriter, TopTrendingWriter>()
                .AddScoped<ITopTrendingReader, TopTrendingReader>()
                .AddAutoMapper(typeof(TopTrendingReader).Assembly);
        }

        public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
        {
            return services
                .AddDbContext<ApplicationContext>(ctx => ctx.UseInMemoryDatabase("AppDatabase"))
                .AddScoped<IRepository<TrendingRepositories>, TrendingRepositoriesRepository>();
        }
    }
}
