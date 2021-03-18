using System;
using System.Collections.Generic;

namespace PackagesExplorer.Application.Models.Outputs
{
    public class InvalidSolutionOutputDto
    {
        public string Uri { get; set; }

        public DateTime ScrappingDate { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public IEnumerable<InvalidProjectOutputDto> InvalidProjects { get; set; }
    }
}
