using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CMS.Core
{

    internal static class AssemblyExtensions
    {

        public static bool HasCmsModules(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Any(IsLoadableModule);
        }

        public static IEnumerable<IModule> GetCmsModules(this Assembly assembly)
        {
            return assembly.GetExportedTypes()
                    .Where(IsLoadableModule)
                    .Select(type => Activator.CreateInstance(type) as IModule);
        }

        private static bool IsLoadableModule(Type type)
        {
            return typeof(IModule).IsAssignableFrom(type)
                && !type.IsAbstract
                && !type.IsInterface
                && type.GetConstructor(Type.EmptyTypes) != null;
        }

    }

}