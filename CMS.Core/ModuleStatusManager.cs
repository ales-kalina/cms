using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using NLog;

namespace CMS.Core
{

    public class ModuleStatusManager : IModuleStatusManager
    {

        private List<ModuleStatus> mItems = new List<ModuleStatus>();
        private string mStoreFilePath;
        
        private Logger mLog = LogManager.GetLogger("ModuleStatusManager"); 

        public ModuleStatusManager(string storeFilePath)
        {
            mStoreFilePath = storeFilePath;
            RestoreItems();
        }

        public IEnumerable<IModule> GetInstalledModules(IEnumerable<IModule> modules)
        {
            return modules.Where(module => !mItems.Any(status => status.ModuleName == module.Name));
        }

        public IEnumerable<IModule> GetUpdatedModules(IEnumerable<IModule> modules)
        {
            List<IModule> result = new List<IModule>();
            foreach (IModule module in modules)
            {
                ModuleStatus status = mItems.SingleOrDefault(x => x.ModuleName == module.Name);
                if (status != null)
                {
                    ModuleStatus currentStatus = ModuleStatus.FromModule(module);
                    if (status.AssemblyFullName != currentStatus.AssemblyFullName || status.AssemblyVersion != currentStatus.AssemblyVersion)
                    {
                        result.Add(module);
                    }
                }
            }
            return result;
        }

        public void RemoveEntriesForModules(IEnumerable<IModule> modules)
        {
            mLog.Debug("Removing modules {0}", String.Join(", ", modules.Select(x => x.Name)));
            IEnumerable<string> moduleNames = modules.Select(x => x.Name);
            mItems.RemoveAll(x => moduleNames.Contains(x.ModuleName));
            StoreItems();
        }

        public void UpdateEntriesForModules(IEnumerable<IModule> modules)
        {
            mLog.Debug("Updating modules {0}", String.Join(", ", modules.Select(x => x.Name)));
            foreach (IModule module in modules)
            {
                ModuleStatus currentStatus = ModuleStatus.FromModule(module);
                ModuleStatus status = mItems.SingleOrDefault(x => x.ModuleName == module.Name);
                if (status != null)
                {
                    status.AssemblyFullName = currentStatus.AssemblyFullName;
                    status.AssemblyVersion = currentStatus.AssemblyVersion;
                }
                else
                {
                    mItems.Add(currentStatus);
                }
            }
            StoreItems();
        }

        private void StoreItems()
        {
            XmlSerializer serializer = new XmlSerializer(mItems.GetType());
            using (StreamWriter writer = new StreamWriter(mStoreFilePath))
            {
                serializer.Serialize(writer, mItems);
            }
        }

        private void RestoreItems()
        {
            if (!File.Exists(mStoreFilePath))
            {
                mItems.Clear();
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(mItems.GetType());
                using (StreamReader reader = new StreamReader(mStoreFilePath))
                {
                    mItems = serializer.Deserialize(reader) as List<ModuleStatus>;
                }
            }
        }

    }

}