using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.WebApplication.Models;
using NuGet;
using System.Reflection;
using System.IO;
using System.Xml;
using CMS.Core;

namespace CMS.WebApplication.Pages
{

    public partial class LocalPackages : System.Web.UI.Page
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
            LocalPackageList.Source = CreateModel();
            LocalPackageList.DataBind();
        }

        private void PackageSearchForm_Search(object sender, EventArgs e)
        {
            SearchTerms = PackageSearchForm.SearchTerms;
        }

        private IEnumerable<LocalPackageModel> CreateModel() {
            IApplicationPackageManager packageManager = Locator.Get<IApplicationPackageManager>();
            IEnumerable<IPackage> packages = packageManager.SearchLocalPackages(SearchTerms).OrderBy(p => p.Id);
            ILocalPackageModelProvider modelProvider = Locator.Get<ILocalPackageModelProvider>();

            return modelProvider.CreateModels(packages);
        }

    }

}