using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CMS.Core
{
 
    public sealed class ModuleDataVersionManager : IModuleDataVersionManager
    {

        private List<ModuleDataVersion> mItems = new List<ModuleDataVersion>();
        private string mStoreFilePath;

        public ModuleDataVersionManager(string storeFilePath)
        {
            mStoreFilePath = storeFilePath;
            RestoreItems();
        }

        public int GetModuleDataVersion(IModule module)
        {
            ModuleDataVersion item = mItems.SingleOrDefault(x => x.ModuleName == module.Name);
            if (item == null)
            {
                return 0;
            }

            return item.DataVersion;
        }

        public void SetModuleDataVersion(IModule module, int version)
        {
            ModuleDataVersion item = mItems.SingleOrDefault(x => x.ModuleName == module.Name);
            if (item == null)
            {
                item = new ModuleDataVersion
                {
                    ModuleName = module.Name,
                    DataVersion = 0
                };
                mItems.Add(item);
            }
            item.DataVersion = version;
            StoreItems();
        }

        public void RemoveEntriesForModules(IEnumerable<IModule> modules)
        {
            mItems.RemoveAll(x => modules.Any(m => x.ModuleName == m.Name));
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
                    mItems = serializer.Deserialize(reader) as List<ModuleDataVersion>;
                }
            }
        }

    }

}