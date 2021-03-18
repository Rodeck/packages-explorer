using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackagesExplorer.Application.Models.Inputs
{
    public class PackageDto
    {
        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public bool Failed { get; set; }
    }
}
