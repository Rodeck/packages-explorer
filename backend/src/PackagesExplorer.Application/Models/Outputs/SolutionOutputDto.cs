using System;
using System.Collections.Generic;

namespace PackagesExplorer.Application.Models.Outputs
{
    public class SolutionOutputDto
    {
        public string Uri { get; set; }

        public string Stars { get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        public IEnumerable<ProjectOutputDto> Projects { get; set; }
    }
}
