using System;
using System.Collections.Generic;

namespace PackagesExplorer.DataAccess
{
    public class TrendingRepositories
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Repository> Repositories { get; set; }
    }

    public class Repository
    {
        public Guid Id { get; set; }

        public string Uri { get; set; }
    }
}
