using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using CMS.WebApplication.Models;
using NuGet;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class LocalPackageUpdates : System.Web.UI.Page
    {

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            string packageId = RouteData.Values["id"].ToString();
            LocalPackageUpdateList.Source = CreateModel(packageId);
            LocalPackageUpdateList.DataBind();
        }

        private IEnumerable<LocalPackageUpdateModel> CreateModel(string packageId)
        {
            IPackage package = Locator.Get<IApplicationPackageManager>().LocalRepository.FindPackage(packageId);
            IEnumerable<IPackage> updatePackages = Locator.Get<IApplicationPackageManager>().SourceRepository.GetUpdates(new IPackage[] { package }, includePrerelease: false, includeAllVersions: true, targetFrameworks: new FrameworkName[] { VersionUtility.DefaultTargetFramework });
            return updatePackages.Select(x => new LocalPackageUpdateModel { Package = package, UpdatePackage = x });
        }

    }

}