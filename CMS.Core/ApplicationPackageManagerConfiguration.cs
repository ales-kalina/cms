using NuGet;

namespace CMS.Core
{

    public sealed class ApplicationPackageManagerConfiguration
    {

        public string LocalPackageSource;
        public string SourcePackageSource;
        public string ConfigurationFileName;
        public IProjectSystem Project;

    }

}