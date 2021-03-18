using System;
using System.Collections.Generic;

namespace PackagesExplorer.Models.Outputs
{
    public class TrendingRepositories
    {
        public IEnumerable<string> Repositories { get; set; }

        public DateTime Date { get; set; }
    }
}
