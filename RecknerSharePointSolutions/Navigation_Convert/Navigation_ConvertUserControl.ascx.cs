using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace RecknerSharePointSolutions.Navigation_Convert
{
    public partial class Navigation_ConvertUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {

                HyperLinkConvertToAJob.NavigateUrl = Request.Url.ToString().Substring(0, Request.Url.ToString().LastIndexOf("/")) + "/convert2Job.aspx";
                           
            
            }

        }
    }
}
