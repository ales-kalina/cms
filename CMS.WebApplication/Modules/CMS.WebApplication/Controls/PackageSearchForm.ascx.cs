using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.WebApplication.Controls
{

    public partial class PackageSearchForm : System.Web.UI.UserControl
    {

        public event EventHandler Search;

        public string SearchTerms
        {
            get
            {
                return TextBoxSearchTerms.Text;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ButtonSearch.Click += new EventHandler(ButtonSearch_Click);
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            OnSearch(EventArgs.Empty);
        }

        protected virtual void OnSearch(EventArgs arguments)
        {
            EventHandler handler = Search;
            if (handler != null)
            {
                handler(this, arguments);
            }
        }

    }

}