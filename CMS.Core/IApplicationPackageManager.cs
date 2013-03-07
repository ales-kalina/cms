using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using NuGet;

namespace CMS.Core
{
    
    public interface IApplicationPackageManager
    {

        IPackageRepository LocalRepository { get; }
        IPackageRepository SourceRepository { get; }
        FrameworkName TargetFramework { get; }
        
        IEnumerable<IPackage> GetSourcePackageDependencies(IPackage package);
        IEnumerable<IPackage> GetSourcePackageDependenciesRequiringLicenseAcceptance(IPackage package);
        IEnumerable<PackageManagerMessage> InstallPackage(IPackage package);
        IQueryable<IPackage> SearchLocalPackages(string searchTerms);
        IQueryable<IPackage> SearchSourcePackages(string searchTerms);
        IEnumerable<PackageManagerMessage> UninstallPackage(IPackage package);
        IEnumerable<PackageManagerMessage> UpdatePackage(IPackage package);
    
    }

}