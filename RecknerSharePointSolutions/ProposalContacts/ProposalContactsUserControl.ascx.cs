using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using RecknerSharePointSolutions.Models;

namespace RecknerSharePointSolutions.ProposalContacts
{
    public partial class ProposalContactsUserControl : UserControl
    {
        SPList proposalContactList;
        SPWeb currentWeb;
        SPList contactList;
        SPWeb blueberryWeb;

        void displayContacts() {

            blueberryWeb = SPContext.Current.Site.OpenWeb("Blueberry");

            contactList = blueberryWeb.Lists["ClientContacts"];

            currentWeb = SPContext.Current.Site.OpenWeb();

            proposalContactList = currentWeb.Lists["ProposalContacts"];

            ArrayList arrDisplayContacts = new ArrayList();


            foreach (SPListItem a in contactList.Items)
            {
                foreach (SPListItem b in proposalContactList.Items)
                {

                    if (a.UniqueId.ToString() == b["Title"].ToString())
                    {

                        var pContact = new ProposalContact();

                        pContact.Id = a.UniqueId;
                        pContact.FullName = a["LinkTitleNoMenu"].ToString() + " " + a["LinkTitle"];
                        pContact.Phone = a.Fields["Phone"].GetFieldValueAsText(a["Phone"]);
                        pContact.Email = a.Fields["Email"].GetFieldValueAsText(a["Email"]);
                        arrDisplayContacts.Add(pContact);


                        //Convert list item to an object. .......................................................................................

                    }
                }

            }

            GridView1.DataSource = arrDisplayContacts;
            GridView1.DataBind();
        
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {

                SPWeb currentWeb = SPContext.Current.Web;

                HyperLink1.NavigateUrl = currentWeb.Url +  "/pages/assigncontacts.aspx";

              SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    displayContacts();
              
                }); 
            
            }

           
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.FindControl("btnRemove");

                btn.CommandArgument = e.Row.RowIndex.ToString();

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
           {
               var rowIndex = int.Parse(e.CommandArgument.ToString());

               var contactID = new Guid(GridView1.DataKeys[rowIndex].Value.ToString());

               blueberryWeb = SPContext.Current.Site.OpenWeb("Blueberry");

               currentWeb = SPContext.Current.Site.OpenWeb();

               proposalContactList = currentWeb.Lists["ProposalContacts"];

               var tmp = proposalContactList.Items.OfType<SPListItem>();

               var rmv = tmp.SingleOrDefault(x => x["ContactID"].ToString().Equals(contactID.ToString()));

               proposalContactList.Items.DeleteItemById(rmv.ID);
               

           });
           
            displayContacts();
             

        }
        
    }
}
