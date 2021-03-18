using System;
using System.Collections.Generic;
using System.Linq;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public class SolutionDao
    {
        public string Url { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth => TotalPackages == 0 ? 1 : (TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<ProjectDao> Projects { get; set; } = new List<ProjectDao>();
    }

    public class ProjectDao
    {
        public string Url { get; set; }

        public IEnumerable<PackageDao> Packages { get; set; } = new List<PackageDao>();
    }

    public class PackageDao
    {
        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public bool Failed { get; set; }
    }
}
