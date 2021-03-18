using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library.Abstraction;
using PackagesExplorer.Models.Outputs;

namespace PackagesExplorer.Library
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionsStore solutionsStore;

        public SolutionService(ISolutionsStore solutionsStore)
        {
            this.solutionsStore = solutionsStore;
        }

        public async Task<ApiResponse<IEnumerable<Solution>>> GetSolutions(string packageName, CancellationToken cancellationToken = default)
        {
            var solutions = await this.solutionsStore.GetSolutions(cancellationToken);

            var filteredSolutions = solutions.Where(s => s.Projects.SelectMany(p => p.Packages).Any(p =>
                p.PackageName.Equals(packageName, StringComparison.InvariantCultureIgnoreCase)));

            var mappedSolutions = filteredSolutions.Select(s => new Solution()
            {
                Uri = s.Url,
                About = s.About,
                Branches = s.Branches,
                Commits = s.Commits,
                LastCommitDate = s.LastCommitDate,
                Stars = s.Stars,
                Projects = s.Projects.Select(p => new Project()
                {
                    Uri = p.Url
                })
            });

            return ApiResponse<IEnumerable<Solution>>.Success(mappedSolutions);

        }

        public async Task<ApiResponse<IEnumerable<Solution>>> GetSolutions(string packageName, string version, CancellationToken cancellationToken = default)
        {
            var solutions = await this.solutionsStore.GetSolutions(cancellationToken);

            var filteredSolutions = solutions.Where(s => s.Projects.SelectMany(p => p.Packages).Any(p =>
                p.PackageName.Equals(packageName, StringComparison.InvariantCultureIgnoreCase) && p.PackageVersion.Equals(version, StringComparison.InvariantCultureIgnoreCase)));

            var mappedSolutions = filteredSolutions.Select(s => new Solution()
            {
                Uri = s.Url,
                Projects = s.Projects.Select(p => new Project()
                {
                    Uri = p.Url
                })
            });

            return ApiResponse<IEnumerable<Solution>>.Success(mappedSolutions);
        }
    }
}
