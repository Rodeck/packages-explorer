using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagesExplorer.Application.Models.Inputs
{
    public class ProjectDto
    {
        public string Url { get; set; }

        public IEnumerable<PackageDto> Packages { get; set; } = new List<PackageDto>();
    }
}
