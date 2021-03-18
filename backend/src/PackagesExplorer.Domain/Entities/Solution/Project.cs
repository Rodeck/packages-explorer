using System.Collections.Generic;

namespace PackagesExplorer.Domain.Entities.Solution
{
    public class Project
    {
        public string Url { get; set; }

        public IEnumerable<Package> Packages { get; set; }
    }
}