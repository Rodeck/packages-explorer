using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using PackagesExplorer.Application.Dao;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.DataAccess
{
    public class JsonFileStore : IStore
    {
        private const string JsonPath = "solutions.json";
        private const string InvalidJsonPath = "solutions_invalid.json";

        private static readonly SemaphoreSlim lockObject = new SemaphoreSlim(1, 1);
        private static readonly SemaphoreSlim lockInvalidObject = new SemaphoreSlim(1, 1);

        public async Task CreateSolution(SolutionInputDto solution, CancellationToken cancellationToken = default)
        {
            await lockObject.WaitAsync(cancellationToken);
            try
            {
                var solutions = await this.GetSolutionsInternal(cancellationToken);

                if (!solutions.Any(s => s.Url == solution.Url))
                {
                    var newSolutions = solutions.Concat(new SolutionInputDto[] {solution});

                    var stringContent = JsonSerializer.Serialize(newSolutions);
                    await File.WriteAllTextAsync(JsonPath, stringContent, cancellationToken);

                }
            }
            finally
            {
                lockObject.Release();
            }
        }

        public async Task<IEnumerable<SolutionInputDto>> GetSolutions(CancellationToken cancellationToken = default)
        {
            await lockObject.WaitAsync(cancellationToken);

            try
            {
                if (!File.Exists(JsonPath))
                {

                    return Array.Empty<SolutionInputDto>();
                }

                using var reader = new StreamReader(JsonPath);
                var json = await reader.ReadToEndAsync();

                return JsonSerializer.Deserialize<IEnumerable<SolutionInputDto>>(json);
            }
            finally
            {
                lockObject.Release();
            }
        }

        private async Task<IEnumerable<SolutionInputDto>> GetSolutionsInternal(CancellationToken cancellationToken = default)
        {
            if (!File.Exists(JsonPath))
            {

                return Array.Empty<SolutionInputDto>();
            }

            using var reader = new StreamReader(JsonPath);
            var json = await reader.ReadToEndAsync();

            return JsonSerializer.Deserialize<IEnumerable<SolutionInputDto>>(json);
        }

        public async Task CreateInvalidSolution(InvalidSolutionDao solution, CancellationToken cancellationToken = default)
        {
            var solutions = await this.GetInvalidSolutions(cancellationToken);

            var newSolutions = solutions.Concat(new InvalidSolutionDao[] { solution });

            var stringContent = JsonSerializer.Serialize(newSolutions);

            await lockInvalidObject.WaitAsync(cancellationToken);

            try
            {
                await File.WriteAllTextAsync(InvalidJsonPath, stringContent, cancellationToken);
            }
            finally
            {
                lockInvalidObject.Release();
            }
        }

        public async Task<IEnumerable<InvalidSolutionDao>> GetInvalidSolutions(CancellationToken cancellationToken = default)
        {
            await lockInvalidObject.WaitAsync(cancellationToken);

            try
            {
                if (!File.Exists(InvalidJsonPath))
                {

                    return Array.Empty<InvalidSolutionDao>();
                }

                using var reader = new StreamReader(InvalidJsonPath);
                var json = await reader.ReadToEndAsync();

                return JsonSerializer.Deserialize<IEnumerable<InvalidSolutionDao>>(json);
            }
            finally
            {
                lockInvalidObject.Release();
            }
        }
    }
}
