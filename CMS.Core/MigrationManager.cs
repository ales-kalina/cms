using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CMS.Core
{

    public sealed class MigrationManager : IMigrationManager
    {

        private readonly Regex mUpgradeMethodNameRegex = new Regex(@"^UpgradeFrom(?<Version>\d+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public int UpgradeModule(IModule module, int currentVersion)
        {
            IMigration migration = GetMigration(module);
            
            if (migration == null)
            {
                return 0;
            }

            Dictionary<int, MethodInfo> steps = GetMigrationSteps(migration);
            MethodInfo step = GetMigrationStep(currentVersion, steps);
            while (step != null)
            {
                currentVersion = (int)step.Invoke(migration, null);
                step = GetMigrationStep(currentVersion, steps);
            }

            return currentVersion;
        }

        private MethodInfo GetMigrationStep(int currentVersion, Dictionary<int, MethodInfo> steps)
        {
            MethodInfo step = null;
            if (!steps.TryGetValue(currentVersion, out step))
            {
                return null;
            }

            return step;
        }

        private Dictionary<int, MethodInfo> GetMigrationSteps(IMigration migration)
        {
            Dictionary<int, MethodInfo> steps = new Dictionary<int, MethodInfo>();
            foreach (MethodInfo method in migration.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public))
            {
                Match match = mUpgradeMethodNameRegex.Match(method.Name);
                if (match.Success && method.ReturnType == typeof(int))
                {
                    int version = 0;
                    if (int.TryParse(match.Groups["Version"].Value, out version))
                    {
                        steps.Add(version, method);
                    }
                }
            }

            return steps;
        }

        private IMigration GetMigration(IModule module)
        {
            Type migrationType = module.GetType().Assembly.GetExportedTypes().Where(x => IsMigration(x)).SingleOrDefault();
            
            if (migrationType == null)
            {
                return null;
            }

            return Activator.CreateInstance(migrationType) as IMigration;
        }

        private static bool IsMigration(Type type)
        {
            return typeof(IMigration).IsAssignableFrom(type)
                && !type.IsAbstract
                && !type.IsInterface
                && type.GetConstructor(Type.EmptyTypes) != null;
        }

    }

}