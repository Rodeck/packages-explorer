using System;
using System.Collections.Generic;

namespace PackagesExplorer.Application.Models.Outputs
{
    public class TrendingRepositoriesOutputDto
    {
        public IEnumerable<string> Repositories { get; set; }

        public DateTime Date { get; set; }
    }
}
