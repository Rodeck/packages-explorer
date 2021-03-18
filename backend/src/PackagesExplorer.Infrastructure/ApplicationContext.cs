using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using PackagesExplorer.Application.Abstraction;
using PackagesExplorer.Application.Dao;
using PackagesExplorer.Domain.Entities.Solution;
using PackagesExplorer.Infrastructure.Dao;

namespace PackagesExplorer.Infrastructure
{
    public class SolutionContext : DbContext, ISolutionContext
    {
        private readonly IMapper mapper;

        public SolutionContext(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public SolutionContext(DbContextOptions<SolutionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SolutionDao> Solutions { get; set; }

        public virtual DbSet<ProjectDao> Projects { get; set; }

        public virtual DbSet<PackageDao> Packages { get; set; }

        public IQueryable<Solution> Entities => this.Solutions.ProjectTo<Solution>(this.mapper.ConfigurationProvider);

        public async Task CreateAsync(Solution solution, CancellationToken cancellationToken = default)
        {
            var solutionDao = this.mapper.Map<SolutionDao>(solution);

            await this.Solutions.AddAsync(solutionDao, cancellationToken);
        }

        public async Task UpdateAsync(Solution solution, CancellationToken cancellationToken = default)
        {
            var existingSolutionDao = await this.Solutions.SingleAsync(s => s.Url == solution.Url, cancellationToken);
            var newSolutionDao = this.mapper.Map<SolutionDao>(solution);

            existingSolutionDao.About = newSolutionDao.About;
            existingSolutionDao.Branches = newSolutionDao.Branches;
            existingSolutionDao.Commits = newSolutionDao.Commits;
            existingSolutionDao.FailedPackages = newSolutionDao.FailedPackages;
            existingSolutionDao.LastCommitDate = newSolutionDao.LastCommitDate;
            existingSolutionDao.PackagesHealth = newSolutionDao.PackagesHealth;
            existingSolutionDao.Stars = newSolutionDao.Stars;
            existingSolutionDao.TotalPackages = newSolutionDao.TotalPackages;

            this.Solutions.Update(existingSolutionDao);
        }

        public Task DeleteAsync(Solution solution, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public new Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        private Solution Merge(SolutionDao to, SolutionDao from)
        {
            to.About = from.About;
            to.Branches = from.Branches;
            to.Commits = from.Commits;
            to.FailedPackages = from.FailedPackages;
            to.LastCommitDate = from.LastCommitDate;
            to.PackagesHealth = from.PackagesHealth;
            to.Stars = from.Stars;
            to.TotalPackages = from.TotalPackages;

            foreach (var newProject in GetNewProjects(to.Projects, from.Projects))
            {
                to.Projects.Add(newProject);
            }

            return to;
        }

        private IEnumerable<ProjectDao> GetNewProjects(ICollection<ProjectDao> toProjects, ICollection<ProjectDao> fromProjects)
        {
            return fromProjects.Where(p => toProjects.All(tp => p.Url != tp.Url));
        }
        private IEnumerable<ProjectDao> GetProjectsToRemove(ICollection<ProjectDao> toProjects, ICollection<ProjectDao> fromProjects)
        {
            return fromProjects.Where(p => toProjects.All(tp => p.Url != tp.Url));
        }
    }
}
