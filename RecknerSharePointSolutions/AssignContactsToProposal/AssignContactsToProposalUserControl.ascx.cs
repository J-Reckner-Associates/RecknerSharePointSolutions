using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;

namespace RecknerSharePointSolutions.AssignContactsToProposal
{
    public partial class AssignContactsToProposalUserControl : UserControl
    {
            string ClientID = "";
            string ProposalID = "";
            string ListID = "";


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

                }

                if (currentWeb.Properties["ProposalID"] != null)
                {

                    ProposalID = currentWeb.Properties["ProposalID"].ToString();

                }

                DataView dv = contactList.Items.GetDataTable().AsDataView();


                if (ClientID != string.Empty)
                {

                    dv.RowFilter = "ClientID='" + ClientID + "'";
                }
                                
                GridView1.DataSource = dv;
                GridView1.DataBind();
                }
                catch (Exception ex)
                {

                    lblMessage.Text = ex.Message;
                }
                
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AssignContact") {

                try
                {

                var rowIndex =  int.Parse(e.CommandArgument.ToString());

                ListID = GridView1.DataKeys[rowIndex].Value.ToString();
               
                var site = SPContext.Current.Site;
               
                var web = site.OpenWeb("Blueberry");

                SPList clientContactList = web.Lists["ClientContacts"];
                
                int selectedID = int.Parse(ListID);

                SPListItem selectedContactListItem = clientContactList.GetItemById(selectedID);

                SPWeb currentWeb = SPContext.Current.Web;
                SPList proposalContactList = currentWeb.Lists["ProposalContacts"];
                SPListItem refContact = proposalContactList.Items.Add();

                refContact["ContactID"] = selectedContactListItem.UniqueId;
           //   refContact["ContactName"] = selectedContactListItem["FirstName"].ToString() + " " + selectedContactListItem["LastName"].ToString();


                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    var tmp = proposalContactList.Items;

                    var contactExist = false;
                    foreach (SPListItem item in tmp)
                    {

                        if (item["ContactID"].ToString() == selectedContactListItem.UniqueId.ToString()) {

                            contactExist = true;
                        
                        }

                    }

                    if (!contactExist) {

                        refContact.Update();
                        Response.Redirect(currentWeb.Url);
                    }
                      
                });

                }
                catch (Exception ex)
                {

                    lblMessage.Text = ex.Message;
                }    
                                  
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnAssignContact");

                btn.CommandArgument = e.Row.RowIndex.ToString();
                               
            }
        }


        private SPListItem CopyItem(SPListItem sourceItem, string destinationListName)
        {
            //Copy sourceItem to destinationList
            SPList destinationList = sourceItem.Web.Lists[destinationListName];
            SPListItem targetItem = destinationList.Items.Add();
            foreach (SPField f in sourceItem.Fields)
            {
                //Copy all except attachments.
                if (!f.ReadOnlyField && f.InternalName != "Attachments"
                    && null != sourceItem[f.InternalName])
                {
                    targetItem[f.InternalName] = sourceItem[f.InternalName];
                }
            }
            //Copy attachments
            foreach (string fileName in sourceItem.Attachments)
            {
                SPFile file = sourceItem.ParentList.ParentWeb.GetFile(sourceItem.Attachments.UrlPrefix + fileName);
                byte[] imageData = file.OpenBinary();
                targetItem.Attachments.Add(fileName, imageData);
            }

            return targetItem;
        }



    }
 }