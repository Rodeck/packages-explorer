using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PackagesExplorer.Library.Abstraction;
using PackagesExplorer.Models.Inputs;

namespace PackagesExplorer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionController : ControllerBase
    {
        private readonly IPackagesService packagesService;

        public SolutionController(IPackagesService packagesService)
        {
            this.packagesService = packagesService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSolution([FromBody] Solution solution, CancellationToken token)
        {
            return Ok(await this.packagesService.CreateSolution(solution, token));
        }

        [HttpGet]
        public async Task<ActionResult> GetInvalidSolutions(CancellationToken token)
        {
            var solutions = await this.packagesService.GetInvalidSolutions(token);
            return Ok(solutions);
        }
    }
}
