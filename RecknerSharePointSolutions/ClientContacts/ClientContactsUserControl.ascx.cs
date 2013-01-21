using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;

namespace RecknerSharePointSolutions.ClientContacts
{
    public partial class ClientContactsUserControl : UserControl
    {
        string ClientID = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {

                try
                {

                    SPWeb blueberryWeb = SPContext.Current.Site.OpenWeb("Blueberry");

                    SPList contactList = blueberryWeb.Lists["ClientContacts"];

                    SPWeb currentWeb = SPContext.Current.Site.OpenWeb();

                    if (currentWeb.Properties["ClientID"] != null)
                    {

                        ClientID = currentWeb.Properties["ClientID"].ToString();

                        HyperLink1.NavigateUrl = blueberryWeb.Url + "/lists/ClientContacts/newform.aspx?ClientID=" + ClientID;
                        HyperLink2.NavigateUrl = blueberryWeb.Url + "/lists/ClientContacts/AllItems.aspx";

                    }


                    if (contactList.ItemCount > 0)
                    {


                        DataView dv = contactList.Items.GetDataTable().AsDataView();


                        if (ClientID != string.Empty)
                        {

                            dv.RowFilter = "ClientID='" + ClientID + "'";
                        }

                        GridView1.DataSource = dv;
                        GridView1.DataBind();

                    }


                }
                catch (Exception ex)
                {

                    lblMessage.Text = ex.Message;
                }

            }
        }
    }
}
