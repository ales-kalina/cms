using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using NuGet;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class UpdateLocalPackage : System.Web.UI.Page
    {

        protected IEnumerable<PackageManagerMessage> Messages { get; private set; }
        protected IPackage LocalPackage { get; private set; }
        protected IPackage SourcePackage { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string packageId = Page.RouteData.Values["id"].ToString();
            SemanticVersion version = SemanticVersion.Parse(Page.RouteData.Values["version"].ToString());
            UpdatePackage(packageId, version);
            DataBind();
        }

        private void UpdatePackage(string packageId, SemanticVersion version)
        {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IApplicationManager manager = Locator.Get<IApplicationManager>();

            LocalPackage = packageManager.LocalRepository.FindPackage(packageId);
            SourcePackage = packageManager.SourceRepository.FindPackage(packageId, version);
            Messages = manager.UpdatePackage(SourcePackage);
        }

    }

}