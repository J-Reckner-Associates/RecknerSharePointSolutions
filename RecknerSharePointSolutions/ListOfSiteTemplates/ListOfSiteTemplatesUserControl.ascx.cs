using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace RecknerSharePointSolutions.ListOfSiteTemplates
{
    public partial class ListOfSiteTemplatesUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                SPSite oSite = SPContext.Current.Site;

                SPWebTemplateCollection wtc = oSite.GetWebTemplates(1033);

                GridView1.DataSource = wtc;
                GridView1.DataBind();


            }
        }
    }
}
