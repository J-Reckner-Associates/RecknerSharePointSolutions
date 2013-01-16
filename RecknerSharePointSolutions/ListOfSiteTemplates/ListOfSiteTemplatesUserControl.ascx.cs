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

                SPWeb rootWeb = oSite.RootWeb;



                if (rootWeb.Properties.ContainsKey("ClientSiteTemplateName"))
                {

                    txtClientSiteTemplate.Text = rootWeb.Properties["ClientSiteTemplateName"].ToString();

                }

 

                if (rootWeb.Properties.ContainsKey("ProposalSiteTemplateName"))
                {

                    txtProposalTemplate.Text = rootWeb.Properties["ProposalSiteTemplateName"].ToString();

                }

 
                SPWebTemplateCollection wtc = oSite.GetWebTemplates(1033);

                GridView1.DataSource = wtc;
                GridView1.DataBind();


            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SPSite oSite = SPContext.Current.Site;

            SPWeb rootWeb = oSite.RootWeb;


            if (!rootWeb.Properties.ContainsKey("ClientSiteTemplateName"))
            {

                rootWeb.Properties.Add("ClientSiteTemplateName", "");
                rootWeb.Properties.Update();

            }
 

            if (!rootWeb.Properties.ContainsKey("ProposalSiteTemplateName"))
            {

                rootWeb.Properties.Add("ProposalSiteTemplateName", "");
                rootWeb.Properties.Update();
            }

  
            if (txtClientSiteTemplate.Text != string.Empty) {

                rootWeb.Properties["ClientSiteTemplateName"] = txtClientSiteTemplate.Text.Trim();
            
            }


            if (txtProposalTemplate.Text != string.Empty) {

                rootWeb.Properties["ProposalSiteTemplateName"] = txtProposalTemplate.Text.Trim();
            
            }

            rootWeb.Properties.Update();


        }
    }
}
