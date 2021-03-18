using System;
using System.Collections.Generic;
using System.Linq;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.Models.Outputs
{
    public class InvalidSolution
    {
        public string Uri { get; set; }

        public DateTime ScrappingDate { get; set; }

        public int FailedPackages => this.InvalidProjects.Sum(p => p.FailedPackages);

        public int TotalPackages => this.InvalidProjects.Sum(p => p.TotalPackages);

        public float PackagesHealth => (TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<InvalidProject> InvalidProjects { get; set; }

        public static InvalidSolution Map(InvalidSolutionDao solution)
        {
            return new InvalidSolution()
            {
                ScrappingDate = solution.ScrappingDate,
                Uri = solution.Uri,
                InvalidProjects = solution.InvalidProjects.Select(p => new InvalidProject()
                {
                    Uri = p.Uri,
                    FailedPackages = p.FailedPackages,
                    InvalidPackages = p.InvalidPackages.Select(pg => new InvalidPackage()
                    {
                        Version = pg.Version,
                        Name = pg.Name,
                    }),
                    TotalPackages = p.TotalPackages,
                })
            };
        }
    }

    public class InvalidProject
    {
        public string Uri { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth => (TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<InvalidPackage> InvalidPackages { get; set; }
    }

    public class InvalidPackage
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
