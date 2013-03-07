using System;
using System.IO;
using System.Web;
using CMS.Core;

namespace CMS.WebApplication
{

    internal sealed class WebApplicationManager : ApplicationManagerBase
    {

        public WebApplicationManager(IApplicationPackageManager packageManager, IModuleStatusManager moduleStatusManager, IModuleDataVersionManager moduleDataVersionManager, IModuleProvider moduleProvider)
            : base(packageManager, moduleStatusManager, moduleDataVersionManager, moduleProvider)
        {

        }

        protected override void RestartApplication()
        {
            if (AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted)
            {
                UnloadApplicationDomain();
            }
            else
            {
                if (!TouchConfigurationFile() && TouchBinFolder())
                {
                    throw new ApplicationException("Application could not be restarted");
                }
            }
        }

        private bool UnloadApplicationDomain()
        {
            try
            {
                HttpRuntime.UnloadAppDomain();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TouchConfigurationFile()
        {
            try
            {
                string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "web.config");
                File.SetLastWriteTimeUtc(filePath, DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TouchBinFolder()
        {
            try
            {
                string folderPath = Path.Combine(HttpRuntime.BinDirectory, "Restarts");
                Directory.CreateDirectory(folderPath);
                string filePath = Path.Combine(folderPath, "marker.txt");
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine("Restart on {0}", DateTime.UtcNow);
                    writer.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}