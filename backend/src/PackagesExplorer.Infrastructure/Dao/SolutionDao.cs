using System;
using System.Collections.Generic;

using PackagesExplorer.Infrastructure.Dao;

namespace PackagesExplorer.Application.Dao
{
    public class SolutionDao
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public ICollection<ProjectDao> Projects { get; set; }
    }
}
