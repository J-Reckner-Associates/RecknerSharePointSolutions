using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace RecknerSharePointSolutions.Proposal2Job
{
    [ToolboxItemAttribute(false)]
    public class Proposal2Job : WebPart
    {
        string _jobCreateAppUrl = "";
        string _siteUrl = "";

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"http://dev-home.reckner.com/BlueBerry/ ")]
        public String SiteUrl { get { return _siteUrl; } set { _siteUrl = value; } }

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String JobCreateAppUrl { get { return _jobCreateAppUrl; } set { _jobCreateAppUrl = value; } }


        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/Proposal2Job/Proposal2JobUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
