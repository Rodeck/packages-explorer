using System;
using System.Collections.Generic;

namespace PackagesExplorer.Domain.Entities.Solution
{
    public class Solution
    {
        public Solution()
        {
            this.PackagesHealth = TotalPackages == 0 ? 1 : (TotalPackages - FailedPackages) / TotalPackages;
        }

        public string Url { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public IEnumerable<Project> Projects
        {
            get => projects;
            set
            {
                this.projects = value;
            }
        }

        private IEnumerable<Project> projects;
    }
}
