using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace RecknerSharePointSolutions.Navigation_Proposals
{
    public partial class Navigation_ProposalsUserControl : UserControl
    {
        string clientID = "";
        string clientName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            SPSite site = SPContext.Current.Site;
            SPWeb blueberryWeb = site.AllWebs["Blueberry"];
        

            if (!IsPostBack)
            {

                SPWeb currentWeb = SPContext.Current.Web;

                if (currentWeb.Properties.ContainsKey("ClientName"))
                {

                    clientName = currentWeb.Properties["ClientName"].ToString();

                }

                if (currentWeb.Properties.ContainsKey("ClientID"))
                {
                    clientID = currentWeb.Properties["ClientID"].ToString();

                }
            }


            //   HyperLinkCreateClient.NavigateUrl = blueberryWeb.Url + "/pages/searchclient.aspx";
            HyperLinkCreateProposal.NavigateUrl = blueberryWeb.Url + "/lists/proposals/newform.aspx?ClientID=" + clientID + "&ClientName=" + clientName;
            HyperLinkManageProposals.NavigateUrl = blueberryWeb.Url + "/lists/proposals/AllItems.aspx";

        }
    }
}
