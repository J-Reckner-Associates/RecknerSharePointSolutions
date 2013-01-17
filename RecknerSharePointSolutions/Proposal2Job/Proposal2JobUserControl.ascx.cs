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

        SPSite oSite;

        void populateJobList() {

            

            foreach (SPWeb item in oSite.AllWebs)
            {
                lstJobs.Items.Add(new ListItem(item.Title, item.Url));          
            }

        
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            this.ThisWebPart = this.Parent as Proposal2Job;

            btnMove.Enabled = Context.User.IsInRole(ThisWebPart.AuthorizedRole) || Context.User.IsInRole("Domain Admins") ;

            if (!Page.IsPostBack) {

               oSite = new SPSite("http://dev-work.reckner.com/jobs" + "/" + 2011); //DateTime.Now.Year);

                SPSecurity.RunWithElevatedPrivileges(delegate() {

                    populateJobList();


                });


         
                
             
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            oSite = new SPSite("http://dev-work.reckner.com/jobs" + "/" + 2011); //DateTime.Now.Year);

               SPSecurity.RunWithElevatedPrivileges(delegate() {
               

                   try
                   {
                        SPWeb currentWeb = SPContext.Current.Web;
                        SPWeb destinationWeb = oSite.AllWebs[lstJobs.SelectedItem.Value];

                        SPList sourceList = currentWeb.Lists["Documents"];
                        SPList destionationList = currentWeb.Lists["Shared Documents"];
                        SPListItemCollection sourceItems = sourceList.Items;


                        foreach (SPListItem item in sourceItems)
                        {
                           // item.copy
                        }
                            
 

                   }
                   catch (Exception ex)
                   {

                       lblMessage.Text = ex.Message;
                   }
           });
        }
    }
}
