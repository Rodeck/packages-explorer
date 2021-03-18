using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagesExplorer.Application.Models.Outputs
{
    public class PackageOutputDto
    {
        public string Name { get; set; }

        public string Version { get; set; }

        public IEnumerable<SolutionOutputDto> Solutions { get; set; }
    }
}
