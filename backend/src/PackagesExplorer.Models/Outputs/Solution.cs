using System;
using System.Collections.Generic;

namespace PackagesExplorer.Models.Outputs
{
    public class Package
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public IEnumerable<Solution> Solutions { get; set; }
    }

    public class Solution
    {
        public string Uri { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        public IEnumerable<Project> Projects { get; set; }
    }

    public class Project
    {
        public string Uri { get; set; }
    }
}
