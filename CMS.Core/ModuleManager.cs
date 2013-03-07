using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CMS.Core
{

    internal sealed class ModuleManager
    {

        private readonly Dictionary<string, IModule> mModules = new Dictionary<string, IModule>();

        public void LoadModules(IEnumerable<string> patterns)
        {
            IEnumerable<string> fileNames = patterns.SelectMany(pattern => GetFilesMatchingPattern(pattern));
            IEnumerable<AssemblyName> moduleAssembliesNames = GetAssemblyNames(fileNames, assembly => assembly.HasCmsModules());
            IEnumerable<IModule> modules = moduleAssembliesNames.Select(assembly => Assembly.Load(assembly)).SelectMany(assembly => assembly.GetCmsModules());
            Load(modules);
        }

        public IEnumerable<IModule> GetModules()
        {
            return mModules.Values.ToArray();
        }

        private void Load(IEnumerable<IModule> modules)
        {
            foreach (IModule module in modules)
            {
                if (string.IsNullOrEmpty(module.Name))
                {
                    throw new NotSupportedException("Modules with null or empty names are not supported");
                }
                IModule existingModule;
                if (mModules.TryGetValue(module.Name, out existingModule))
                {
                    throw new NotSupportedException(String.Format("Error loading module '{0}' of type {1}, another module (of type {2}) with the same name has already been loaded", module.Name, module.GetType().FullName, existingModule.Name));
                }
                mModules.Add(module.Name, module);
            }
        }

        private static IEnumerable<string> GetFilesMatchingPattern(string pattern)
        {
            return NormalizePaths(Path.GetDirectoryName(pattern))
                    .SelectMany(path => Directory.GetFiles(path, Path.GetFileName(pattern)));
        }

        private static IEnumerable<string> NormalizePaths(string path)
        {
            return Path.IsPathRooted(path)
                        ? new[] { Path.GetFullPath(path) }
                        : GetBaseDirectories().Select(baseDirectory => Path.Combine(baseDirectory, path));
        }

        private static IEnumerable<string> GetBaseDirectories()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string searchPath = AppDomain.CurrentDomain.RelativeSearchPath;

            return String.IsNullOrEmpty(searchPath)
                ? new[] { baseDirectory }
                : searchPath.Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(path => Path.Combine(baseDirectory, path));
        }

        private static IEnumerable<AssemblyName> GetAssemblyNames(IEnumerable<string> filenames, Predicate<Assembly> filter)
        {
            List<AssemblyName> result = new List<AssemblyName>();
            foreach (var filename in filenames)
            {
                Assembly assembly;
                if (File.Exists(filename))
                {
                    try
                    {
                        assembly = Assembly.LoadFrom(filename);
                    }
                    catch (BadImageFormatException)
                    {
                        continue;
                    }
                }
                else
                {
                    try
                    {
                        assembly = Assembly.Load(filename);
                    }
                    catch (FileNotFoundException)
                    {
                        continue;
                    }
                }

                if (filter(assembly))
                {
                    result.Add(assembly.GetName(false));
                }
            }

            return result;
        }

    }

}