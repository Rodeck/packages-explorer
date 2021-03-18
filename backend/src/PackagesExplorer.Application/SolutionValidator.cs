using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library.Abstraction;
using PackagesExplorer.Library.Options;

namespace PackagesExplorer.Library
{
    public class SolutionValidator : ISolutionValidator
    {
        private readonly IOptions<SolutionValidatorOptions> options;

        public SolutionValidator(IOptions<SolutionValidatorOptions> options)
        {
            this.options = options;
        }

        public Task<bool> Validate(SolutionInputDto solution, out InvalidSolutionDao invalidSolutionModel, CancellationToken token = default)
        {
            // Validate if each package has proper name

            List<InvalidPorojectDao> invalidProjects = null;

            foreach (var project in solution.Projects)
            {
                var invalidPackages = project.Packages.Where(p => string.IsNullOrEmpty(p.PackageName));
                int totalPackages = project.Packages.Count();

                if (!invalidPackages.Any())
                {
                    continue;
                }

                invalidProjects ??= new List<InvalidPorojectDao>();

                var invalidProject = new InvalidPorojectDao()
                {
                    InvalidPackages = invalidPackages.Select(p => new InvalidPackageDao()
                    {
                        Name = p.PackageName,
                        Version = p.PackageVersion,
                    }),
                    FailedPackages = invalidPackages.Count(),
                    TotalPackages = totalPackages,
                    Uri = project.Url
                };

                invalidProjects.Add(invalidProject);

                solution.FailedPackages += invalidPackages.Count();
                solution.TotalPackages += totalPackages;
            }

            if (invalidProjects != null)
            {
                var invalidSolution = new InvalidSolutionDao()
                {
                    Uri = solution.Url,
                    ScrappingDate = DateTime.Now,
                    InvalidProjects = invalidProjects,
                };

                invalidSolutionModel = invalidSolution;

                // strip invalid projects, we will scrap them later
                //solution.Projects = solution.Projects.Where(p => !invalidProjects.Any(ip => p.Url == ip.Uri));

                if (invalidProjects.Count() / solution.Projects.Count() > this.options.Value.FailedPackagesThreshold)
                {
                    return Task.FromResult(false);
                }
            }

            invalidSolutionModel = null;
            return Task.FromResult(true);
        }
    }
}
