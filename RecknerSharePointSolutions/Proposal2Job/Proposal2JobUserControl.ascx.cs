using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace RecknerSharePointSolutions.Proposal2Job
{
    public partial class Proposal2JobUserControl : UserControl
    {
        public Proposal2Job ThisWebPart { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {

            this.ThisWebPart = this.Parent as Proposal2Job;

            if (!Page.IsPostBack) {

                SPWeb currentWeb = SPContext.Current.Web;

                SPFolder documentsFolder = currentWeb.Folders["Documents"];

                string ClientID = "";

                if (currentWeb.Properties.ContainsKey("ClientID")) {

                    ClientID = currentWeb.Properties["ClientID"];
                
                }

                HyperLink1.NavigateUrl = ThisWebPart.JobCreateAppUrl + "?SiteUrl=" + documentsFolder.ParentWeb.Url.ToString() + "&ClientID=" + ClientID;
            
            }
        }
    }
}
