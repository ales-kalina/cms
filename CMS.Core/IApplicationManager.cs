using System.Collections.Generic;
using NuGet;

namespace CMS.Core
{

    public interface IApplicationManager
    {

        IEnumerable<PackageManagerMessage> InstallPackage(IPackage package);
        IEnumerable<PackageManagerMessage> UpdatePackage(IPackage package);
        IEnumerable<PackageManagerMessage> UninstallPackage(IPackage package);

    }

}