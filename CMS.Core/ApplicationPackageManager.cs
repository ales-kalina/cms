using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Xml.Linq;
using NuGet;
using NuGet.Runtime;

namespace CMS.Core
{

    public sealed class ApplicationPackageManager : IApplicationPackageManager
    {

        private IProjectManager mProjectManager;
        private string mConfigurationFileName;

        public IPackageRepository LocalRepository
        {
            get
            {
                return mProjectManager.LocalRepository;
            }
        }

        public IPackageRepository SourceRepository
        {
            get
            {
                return mProjectManager.SourceRepository;
            }
        }

        public FrameworkName TargetFramework
        {
            get
            {
                return mProjectManager.Project.TargetFramework;
            }
        }

        public ApplicationPackageManager(ApplicationPackageManagerConfiguration configuration)
        {
            IPackageRepository localRepository = PackageRepositoryFactory.Default.CreateRepository(configuration.LocalPackageSource);
            IPackageRepository sourceRepository = PackageRepositoryFactory.Default.CreateRepository(configuration.SourcePackageSource);
            IPackagePathResolver pathResolver = new DefaultPackagePathResolver(configuration.LocalPackageSource);
            mProjectManager = new ProjectManager(sourceRepository, pathResolver, configuration.Project, localRepository);
            mConfigurationFileName = configuration.ConfigurationFileName;
        }

        public IQueryable<IPackage> SearchLocalPackages(string searchTerms)
        {
            return GetPackages(LocalRepository, searchTerms);
        }

        public IQueryable<IPackage> SearchSourcePackages(string searchTerms)
        {
            return GetPackages(SourceRepository, searchTerms);
        }

        public IEnumerable<PackageManagerMessage> InstallPackage(IPackage package)
        {
            return PerformLoggedAction(() =>
            {
                mProjectManager.AddPackageReference(package.Id, package.Version, ignoreDependencies: false, allowPrereleaseVersions: false);
                AddBindingRedirects();
            });
        }

        public IEnumerable<PackageManagerMessage> UpdatePackage(IPackage package)
        {
            return PerformLoggedAction(() =>
            {
                mProjectManager.UpdatePackageReference(package.Id, package.Version, updateDependencies: true, allowPrereleaseVersions: false);
                AddBindingRedirects();
            });
        }

        public IEnumerable<PackageManagerMessage> UninstallPackage(IPackage package)
        {
            return PerformLoggedAction(() =>
            {
                mProjectManager.RemovePackageReference(package.Id, forceRemove: false, removeDependencies: false);
                RemoveBindingRedirects();
            });
        }

        public IEnumerable<IPackage> GetSourcePackageDependenciesRequiringLicenseAcceptance(IPackage package)
        {
            IEnumerable<IPackage> dependencies = GetSourcePackageDependencies(package);

            return dependencies.Where(p => p.RequireLicenseAcceptance);
        }

        public IEnumerable<IPackage> GetSourcePackageDependencies(IPackage package)
        {
            InstallWalker walker = new InstallWalker(localRepository: LocalRepository, sourceRepository: SourceRepository, logger: NullLogger.Instance, ignoreDependencies: false, allowPrereleaseVersions: false, targetFramework: null);
            IEnumerable<PackageOperation> operations = walker.ResolveOperations(package);
            
            return operations.Where(operation => operation.Action == PackageAction.Install).Select(operation => operation.Package);
        }

        private IQueryable<IPackage> GetPackages(IPackageRepository repository, string searchTerm)
        {
            return GetPackages(repository.GetPackages(), searchTerm);
        }

        private IQueryable<IPackage> GetPackages(IQueryable<IPackage> packages, string searchTerm)
        {
            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.Trim();
                packages = packages.Find(searchTerm);
            }

            return packages;
        }

        private void AddBindingRedirects()
        {
            IEnumerable<AssemblyBinding> bindingRedirects = BindingRedirectResolver.GetBindingRedirects(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain);
            if (bindingRedirects.Any())
            {
                BindingRedirectManager bindingRedirectManager = new BindingRedirectManager(mProjectManager.Project, mConfigurationFileName);
                bindingRedirectManager.AddBindingRedirects(bindingRedirects);
            }
        }

        private void RemoveBindingRedirects()
        {
            IEnumerable<AssemblyBinding> bindingRedirects = BindingRedirectResolver.GetBindingRedirects(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain);
            string configurationFilePath = Path.Combine(mProjectManager.Project.Root, mConfigurationFileName);
            XDocument document = XDocument.Load(configurationFilePath);
            XElement runtimeElement = document.Root.Element("runtime");
            IEnumerable<XElement> assemblyBindingElements = Enumerable.Empty<XElement>();
            if (runtimeElement != null)
            {
                assemblyBindingElements = runtimeElement.Elements(XName.Get("assemblyBinding", "urn:schemas-microsoft-com:asm.v1")).Elements(XName.Get("dependentAssembly", "urn:schemas-microsoft-com:asm.v1"));
            }
            IEnumerable<AssemblyBinding> configurationBindingRedirects = assemblyBindingElements.Select(x => AssemblyBinding.Parse(x));
            IEnumerable<AssemblyBinding> candidates = configurationBindingRedirects.Except(bindingRedirects);
            if (candidates.Any())
            {
                BindingRedirectManager bindingRedirectManager = new BindingRedirectManager(mProjectManager.Project, mConfigurationFileName);
                bindingRedirectManager.RemoveBindingRedirects(candidates);
            }
        }

        private IEnumerable<PackageManagerMessage> PerformLoggedAction(Action action)
        {
            MessageLogger logger = new MessageLogger();
            mProjectManager.Logger = logger;
            try
            {
                action();
            }
            finally
            {
                mProjectManager.Logger = null;
            }

            return logger.Messages;
        }

        private class MessageLogger : ILogger
        {

            private readonly IList<PackageManagerMessage> mMessages = new List<PackageManagerMessage>();

            public IEnumerable<PackageManagerMessage> Messages
            {
                get
                {
                    return mMessages.AsEnumerable();
                }
            }

            public void Log(MessageLevel level, string message, params object[] arguments)
            {
                string text = String.Format(message, arguments);
                PackageManagerMessage item = new PackageManagerMessage(level, text);
                mMessages.Add(item);
            }

        }

    }

}