using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PackagesExplorer.Domain.Entities.Solution;

namespace PackagesExplorer.Infrastructure.Dao
{
    public class ProjectDao
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public IEnumerable<PackageDao> Packages { get; set; }
    }
}
