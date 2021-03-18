namespace PackagesExplorer.Domain.Entities.Solution
{
    public class Package
    {
        public string PackageName { get; set; }

        public string PackageVersion { get; set; }

        public bool Failed { get; set; }
    }
}