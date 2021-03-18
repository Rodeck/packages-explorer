using System;
using System.Collections.Generic;
using System.Linq;

using PackagesExplorer.Application.Models.Inputs;

namespace PackagesExplorer.DataAccess.Abstraction
{
    public class SolutionInputDto
    {
        public string Url { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        //public int FailedPackages { get; set; }

        //public int TotalPackages { get; set; }

        //public float PackagesHealth => TotalPackages == 0 ? 1 : (TotalPackages - FailedPackages) / TotalPackages;

        public IEnumerable<ProjectInputDto> Projects { get; set; } = new List<ProjectInputDto>();
    }
}
