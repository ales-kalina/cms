using System.Linq;
using System.Reflection;

namespace CMS.Core
{

    public class ModuleStatus
    {

        public string AssemblyFullName { get; set; }
        public string AssemblyVersion { get; set; }
        public string ModuleName { get; set; }

        public static ModuleStatus FromModule(IModule module)
        {
            Assembly assembly = module.GetType().Assembly;
            AssemblyInformationalVersionAttribute attribute = assembly.GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute), false).Cast<AssemblyInformationalVersionAttribute>().SingleOrDefault();
            
            return new ModuleStatus
            {
                AssemblyFullName = assembly.FullName,
                AssemblyVersion = attribute.InformationalVersion,
                ModuleName = module.Name
            };
        }

    }

}