using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using PackagesExplorer.DataAccess.Abstraction;

namespace PackagesExplorer.Models.Inputs
{
    public class Solution
    {
        public string Url { get; set; }

        public string Stars{ get; set; }

        public string About { get; set; }

        public string Commits { get; set; }

        public string Branches { get; set; }

        public DateTime LastCommitDate { get; set; }

        [JsonPropertyName("csprojs")]
        public IEnumerable<Project> Projects { get; set; } = new List<Project>();

        public SolutionDao Map()
        {
            try
            {
                return new SolutionDao()
                {
                    Url = this.Url,
                    About = this.About,
                    Branches = this.Branches,
                    Commits = this.Commits,
                    Stars = this.Stars,
                    LastCommitDate = this.LastCommitDate,
                    Projects = this.Projects.Select(p => new ProjectDao()
                    {
                        Url = p.Url,
                        Packages = p.Packages.Select(pg => new PackageDao()
                        {
                            Failed = pg.Failed,
                            PackageName = pg.PackageName,
                            PackageVersion = pg.PackageVersion,
                        })
                    })
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }

    public class Project
    {
        [JsonPropertyName("projectUrl")]
        public string Url { get; set; }

        [JsonPropertyName("packages")]
        public IEnumerable<Package> Packages { get; set; } = new List<Package>();
    }

    public class Package
    {
        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public bool Failed { get; set; }
    }
}
