using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace RecknerSharePointSolutions.Navigation_Client
{
    public partial class Navigation_ClientUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SPWeb blueberryWeb = SPContext.Current.Site.AllWebs["Blueberry"];
                
                HyperLinkCreateNewClient.NavigateUrl = blueberryWeb.Url + "/pages/searchclient.aspx";
                
            }


        }
    }
}
