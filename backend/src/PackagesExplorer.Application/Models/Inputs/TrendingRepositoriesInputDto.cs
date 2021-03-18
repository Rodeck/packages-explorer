using System.Collections.Generic;

namespace PackagesExplorer.Application.Models.Inputs
{
    public class TrendingRepositoriesInputDto
    {
        public IEnumerable<string> Repositories { get; set; }
    }
}
