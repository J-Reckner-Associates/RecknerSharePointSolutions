using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Publishing;
using Production.Business;
using System.Linq;
using System.Configuration;

namespace RecknerSharePointSolutions.SearchClient
{
    public partial class SearchClientUserControl : UserControl
    {
        public SearchClient ThisWebPart { get; set; }

        ClientInfoList clients;


        protected void Page_Load(object sender, EventArgs e)
        {

            this.ThisWebPart = this.Parent as SearchClient;
            clients = Production.Business.ClientInfoList.GetList();

        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                LinkButton btnCreateInSharePoint = (LinkButton)e.Row.FindControl("btnCreateInSharePoint");
                btnCreateInSharePoint.CommandArgument = e.Row.Cells[0].Text + "|" + e.Row.Cells[1].Text;

            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WebControl btn = (WebControl)e.CommandSource;

            if (btn.ID == "btnCreateInSharePoint")
            {
                LinkButton btnCreateInSharePoint = (LinkButton)btn;

                var cmdArg = btnCreateInSharePoint.CommandArgument.ToString();

                var ClientID = cmdArg.Split(Char.Parse("|"))[0];
                var ClientName = cmdArg.Split(Char.Parse("|"))[1];
                
                var redirectUrl = Shared.Utilities.CreateWebSite(ClientID, ClientName, "", ThisWebPart.SiteTemplate);

                Response.Redirect(redirectUrl);

            }

        }


        protected void txtClientNameSearch_TextChanged(object sender, EventArgs e)
        {
            var cname = txtClientNameSearch.Text.ToUpper();

            if (cname != "")
            {

                var filtredClientList = from c in clients where c.ClientName.ToUpper().StartsWith(cname) select c;

                lblMessage.Text = filtredClientList.Count().ToString() + " Client(s) found";

                if (filtredClientList.Count() == 0)
                {

                    lblMessage.Text += ", would you like to create ? ";
                    btnNewClient.Visible = true;

                }
                else
                {

                    lblMessage.Text = filtredClientList.Count().ToString() + " Client(s) found";
                    btnNewClient.Visible = false;

                }

                GridView1.DataSource = filtredClientList;
                Page.DataBind();

            }


        }

        protected void btnNewClient_Click(object sender, EventArgs e)
        {
            Response.Redirect(ThisWebPart.NewClientFormUrl);
        }

    }
}
