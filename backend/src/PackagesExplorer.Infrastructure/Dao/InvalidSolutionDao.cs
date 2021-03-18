using System;
using System.Collections.Generic;

using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.Application.Dao
{
    public class InvalidSolutionDao
    {
        public string Uri { get; set; }

        public DateTime ScrappingDate { get; set; }

        public int FailedPackages { get; set; }

        public int TotalPackages { get; set; }

        public float PackagesHealth { get; set; }

        public IEnumerable<InvalidProjectDao> InvalidProjects { get; set; }
    }
}
