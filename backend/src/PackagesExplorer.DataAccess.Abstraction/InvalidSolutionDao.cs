using System;
using System.Collections.Generic;
using System.Linq;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public class InvalidSolutionDao
    {
        public string Uri { get; set; }

        public DateTime ScrappingDate { get; set; }

        public int FailedPackages => this.InvalidProjects.Sum(p => p.FailedPackages);

        public int TotalPackages => this.InvalidProjects.Sum(p => p.TotalPackages);

        public float PackagesHealth => (TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<InvalidPorojectDao> InvalidProjects { get; set; }
    }

    public class InvalidPorojectDao
    {
        public string Uri { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth => TotalPackages == 0 ? 1 :(TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<InvalidPackageDao> InvalidPackages { get; set; }
    }

    public class InvalidPackageDao
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
