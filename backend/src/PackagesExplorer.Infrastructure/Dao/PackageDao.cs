using System;

namespace PackagesExplorer.Infrastructure.Dao
{
    public class PackageDao
    {
        public Guid Id { get; set; }

        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public bool Failed { get; set; }
    }
}
