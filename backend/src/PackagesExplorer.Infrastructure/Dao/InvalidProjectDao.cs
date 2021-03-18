using System.Collections.Generic;

using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.Application.Dao
{
    public class InvalidProjectDao
    {
        public string Uri { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public IEnumerable<InvalidPackageDao> InvalidPackages { get; set; }
    }
}