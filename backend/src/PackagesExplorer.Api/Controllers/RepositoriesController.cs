using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PackagesExplorer.Library.Repositories.Abstraction;
using PackagesExplorer.Models.Inputs;

namespace PackagesExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    public class RepositoriesController : ControllerBase
    {
        private readonly ITopTrendingWriter writer;
        private readonly ITopTrendingReader reader;

        public RepositoriesController(ITopTrendingWriter writer, ITopTrendingReader reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        [HttpPost]
        public async Task<IActionResult> SandTrending([FromBody] TrendingRepositories trending, CancellationToken cancellationToken)
        {
            var response = await this.writer.SaveAsync(trending, cancellationToken);

            if (response.Ok)
            {
                return NoContent();
            }

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var response = await this.reader.GetTopTrendingAsync(cancellationToken);

            if (response.Ok)
            {
                return Ok(response);
            }

            return BadRequest(response.Errors);
        }
    }
}
