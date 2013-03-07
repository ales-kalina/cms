using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.WebApplication.Models;
using NuGet;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class SourcePackages : System.Web.UI.Page
    {

        public string SearchTerms
        {
            get
            {
                return (string)(ViewState["SearchTerms"] ?? String.Empty);
            }
            set
            {
                ViewState["SearchTerms"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PackageSearchForm.Search += new EventHandler(PackageSearchForm_Search);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SourcePackageList.Source = CreateModel();
            SourcePackageList.DataBind();
        }

        private void PackageSearchForm_Search(object sender, EventArgs e)
        {
            SearchTerms = PackageSearchForm.SearchTerms;
        }

        private IEnumerable<SourcePackageModel> CreateModel() {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IPackage[] sourcePackages = packageManager.SearchSourcePackages(SearchTerms).Where(x => x.IsLatestVersion).OrderByDescending(x => x.DownloadCount).ThenBy(x => x.Id).ToArray();
            ISourcePackageModelProvider modelProvider = Locator.Get<ISourcePackageModelProvider>();

            return modelProvider.CreateModels(sourcePackages);
        }

    }

}