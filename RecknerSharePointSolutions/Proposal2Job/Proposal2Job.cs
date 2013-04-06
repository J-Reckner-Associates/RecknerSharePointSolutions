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

        private String _ExportLocation = @"c:\Export";
        private String _AuthorizedRole = @"Reckner\SP - Blueberry Moderators";
        private String _CopyMessage = "Copy all setup materials to";
        private String _destinationSiteCollectionUrl = "http://dev-work.reckner.com/jobs";

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String DestionationSiteCollectionUrl { get; set; }

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        [DefaultValue(@"c:\Export")]
        public String ExportLocation { get { return _ExportLocation; } set { _ExportLocation = value; } }

        [WebBrowsable(true)]
        [Personalizable(PersonalizationScope.Shared)]
        [Category("Settings")]
        public String AuthorizedRole { get { return _AuthorizedRole; } set { _AuthorizedRole = value; } }



        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/RecknerSharePointSolutions/Proposal2Job/Proposal2JobUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
