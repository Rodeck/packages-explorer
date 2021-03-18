using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PackagesExplorer.DataAccess.Abstraction;
using PackagesExplorer.Library.Abstraction;
using PackagesExplorer.Models.Inputs;

namespace PackagesExplorer.Library
{
    public class PackagesService : IPackagesService
    {
        private readonly ISolutionsStore solutionsStore;
        private readonly ISolutionValidator validator;

        public PackagesService(ISolutionsStore solutionsStore, ISolutionValidator validator)
        {
            this.solutionsStore = solutionsStore;
            this.validator = validator;
        }

        public async Task<ApiResponse<Solution>> CreateSolution(Solution solution, CancellationToken token = default)
        {
            var daoSolution = solution.Map();

            if (await this.validator.Validate(daoSolution, out var invalidSolution, token))
            {
                if (invalidSolution != null)
                {
                    daoSolution.Projects =
                        daoSolution.Projects.Where(p => invalidSolution.InvalidProjects.All(ip => ip.Uri != p.Url));

                    await this.solutionsStore.SaveInvalidSolution(invalidSolution, token);
                }

                await this.solutionsStore.CreateSolution(daoSolution, token);
            }

            return ApiResponse<Solution>.Success();
        }

        public async Task<ApiResponse<IEnumerable<Models.Outputs.InvalidSolution>>> GetInvalidSolutions(CancellationToken token = default)
        {
            var invalidSoultions = await this.solutionsStore.GetInvalidSolutions(token);

            var map = invalidSoultions.Select(s => Models.Outputs.InvalidSolution.Map(s));

            return ApiResponse<IEnumerable<Models.Outputs.InvalidSolution>>.Success(map);
        }
    }
}
