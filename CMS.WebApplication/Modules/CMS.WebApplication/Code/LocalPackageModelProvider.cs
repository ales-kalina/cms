using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Web;
using System.Xml;
using CMS.Core;
using CMS.WebApplication.Models;
using NuGet;

namespace CMS.WebApplication
{

    internal sealed class LocalPackageModelProvider : ILocalPackageModelProvider
    {

        private IApplicationPackageManager mPackageManager;

        private HashSet<string> mReferencedAssemblyNames;
        private HashSet<string> mReferencedLocalPackageNames;
        private HashSet<string> mProjectPackageNames;

        public LocalPackageModelProvider(IApplicationPackageManager packageManager)
        {
            mPackageManager = packageManager;
            mReferencedAssemblyNames = new HashSet<string>(AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetReferencedAssemblies()).Select(x => x.Name), StringComparer.InvariantCultureIgnoreCase);
            mReferencedLocalPackageNames = new HashSet<string>(mPackageManager.LocalRepository.GetPackages().SelectMany(x => GetReferencedPackageNames(x)), StringComparer.InvariantCultureIgnoreCase);
            mProjectPackageNames = GetProjectPackageNames();
        }

        public LocalPackageModel CreateModel(IPackage package)
        {
            return new LocalPackageModel
            {
                Package = package,
                UpdatePackage = GetUpdate(package),
                HasDependentPackages = mReferencedAssemblyNames.Contains(package.Id) || mReferencedLocalPackageNames.Contains(package.Id),
                IsProjectPackage = mProjectPackageNames.Contains(package.Id)
            };
        }

        public IEnumerable<LocalPackageModel> CreateModels(IEnumerable<IPackage> packages)
        {
            return packages.Select(x => CreateModel(x));
        }

        private IPackage GetUpdate(IPackage package)
        {
            FrameworkName[] frameworkNames = new FrameworkName[] { mPackageManager.TargetFramework };
            IPackage[] packages = new IPackage[] { package };
            IEnumerable<IPackage> updatePackages = mPackageManager.SourceRepository.GetUpdates(packages, includePrerelease: false, includeAllVersions: false, targetFrameworks: frameworkNames);

            return updatePackages.SingleOrDefault();
        }

        private IEnumerable<string> GetReferencedPackageNames(IPackage package)
        {
            PackageDependencySet dependencySet = package.DependencySets.SingleOrDefault(x => x.TargetFramework == null || x.TargetFramework == mPackageManager.TargetFramework);
            if (dependencySet == null)
            {
                return Enumerable.Empty<string>();
            }
            return dependencySet.Dependencies.Select(x => x.Id);
        }

        private HashSet<string> GetProjectPackageNames()
        {
            HashSet<string> packageNames = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            string packagesFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, "packages.config");
            if (File.Exists(packagesFilePath))
            {
                XmlDocument packagesDocument = new XmlDocument();
                packagesDocument.Load(packagesFilePath);
                foreach (XmlNode packageNode in packagesDocument.SelectNodes("//package"))
                {
                    packageNames.Add(packageNode.Attributes["id"].Value);
                }
            }

            return packageNames;
        }

    }

}