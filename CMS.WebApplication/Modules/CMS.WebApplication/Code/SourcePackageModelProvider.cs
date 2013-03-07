using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMS.Core;
using CMS.WebApplication.Models;
using NuGet;

namespace CMS.WebApplication
{

    internal sealed class SourcePackageModelProvider : ISourcePackageModelProvider
    {

        private IApplicationPackageManager mPackageManager;

        private HashSet<string> mAssemblyNames;
        private HashSet<string> mLocalPackageNames;

        public SourcePackageModelProvider(IApplicationPackageManager packageManager)
        {
            mPackageManager = packageManager;
            mAssemblyNames = new HashSet<string>(AppDomain.CurrentDomain.GetAssemblies().Select(x => GetAssemblyName(x)), StringComparer.InvariantCultureIgnoreCase);
            mLocalPackageNames = new HashSet<string>(mPackageManager.LocalRepository.GetPackages().Select(x => x.Id), StringComparer.InvariantCultureIgnoreCase);
        }

        public SourcePackageModel CreateModel(IPackage package)
        {
            return new SourcePackageModel
            {
                Package = package,
                IsInstalled = mAssemblyNames.Contains(package.Id) || mLocalPackageNames.Contains(package.Id)
            };
        }

        public IEnumerable<SourcePackageModel> CreateModels(IEnumerable<IPackage> packages)
        {
            return packages.Select(x => CreateModel(x));
        }

        private string GetAssemblyName(Assembly assembly)
        {
            AssemblyName name = new AssemblyName(assembly.FullName);
            
            return name.Name;
        }

    }

}