using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RecknerSharePointSolutions.NewClientForm
{
    [ToolboxItemAttribute(false)]
    public class NewClientForm : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/NewClientForm/NewClientFormUserControl.ascx";

        string _searchClientUrl = "";
        string _siteTemplateName = "ENTERWIKI#0";

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"http://dev-home.reckner.com/BlueBerry/ ")]
        public String SearchClientUrl { get { return _searchClientUrl; } set { _searchClientUrl = value; } }



        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String SiteTemplate { get { return _siteTemplateName; } set { _siteTemplateName = value; } }



        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
