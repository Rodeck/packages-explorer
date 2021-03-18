using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PackagesExplorer.Library;
using PackagesExplorer.Library.Abstraction;

namespace PackagesExplorer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesExplorerController : ControllerBase
    {
        private readonly ISolutionService solutionService;

        public PackagesExplorerController(ISolutionService solutionService)
        {
            this.solutionService = solutionService;
        }

        [HttpGet]
        [Route("{packageName}")]
        public async Task<ActionResult> GetSolutions(string packageName, CancellationToken cancellationToken)
        {
            var solutions = await this.solutionService.GetSolutions(packageName, cancellationToken);
            return Ok(solutions);
        }
    }
}
