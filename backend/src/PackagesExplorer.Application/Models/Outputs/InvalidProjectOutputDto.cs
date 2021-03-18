using System.Collections.Generic;

namespace PackagesExplorer.Application.Models.Outputs
{
    public class InvalidProjectOutputDto
    {
        public string Uri { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public IEnumerable<InvalidPackageOutputDto> InvalidPackages { get; set; }
    }
}
