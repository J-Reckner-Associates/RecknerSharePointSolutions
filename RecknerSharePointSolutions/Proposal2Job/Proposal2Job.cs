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
        
        string _jobSiteUrl = "";
        string _authorizedRole = "";

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"http://dev-work.reckner.com/jobs ")]
        public String JobSiteUrl { get { return _jobSiteUrl; } set { _jobSiteUrl = value; } }

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String AuthorizedRole { get { return _authorizedRole; } set { _authorizedRole = value; } }



        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/Proposal2Job/Proposal2JobUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
