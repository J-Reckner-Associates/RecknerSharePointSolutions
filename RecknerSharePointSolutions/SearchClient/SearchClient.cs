using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RecknerSharePointSolutions.SearchClient
{
    [ToolboxItemAttribute(false)]
    public class SearchClient : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/SearchClient/SearchClientUserControl.ascx";


        string _siteUrl = "http://dev-home.reckner.com/BlueBerry/";
        string _siteTemplateName = "ENTERWIKI#0";
        string _newClientFormUrl = "";

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String SiteTemplate { get { return _siteTemplateName; } set { _siteTemplateName = value; } }




        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"http://dev-home.reckner.com/BlueBerry/ ")]
        public String SiteUrl { get { return _siteUrl; } set { _siteUrl = value; } }


        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"http://dev-home.reckner.com/BlueBerry/ ")]
        public String NewClientFormUrl { get { return _newClientFormUrl; } set { _newClientFormUrl = value; } }



        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
