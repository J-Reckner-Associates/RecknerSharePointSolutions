using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Production.Business;

namespace RecknerSharePointSolutions.NewClientForm
{
    
    public partial class NewClientFormUserControl : UserControl
    {

        public NewClientForm ThisWebPart { get; set; }

        protected override void OnInit(EventArgs e)
        {
            if (SPContext.Current.FormContext.FormMode == SPControlMode.Edit)
            {

                RequiredFieldValidator1.Enabled = false;
                RequiredFieldValidator10.Enabled = false;
                RequiredFieldValidator11.Enabled = false;
                RequiredFieldValidator2.Enabled = false;
                RequiredFieldValidator3.Enabled = false;
                RequiredFieldValidator4.Enabled = false;
                RequiredFieldValidator5.Enabled = false;
                RequiredFieldValidator6.Enabled = false;
                RequiredFieldValidator7.Enabled = false;
                RequiredFieldValidator8.Enabled = false;
                RequiredFieldValidator9.Enabled = false;
                RegularExpressionValidator1.Enabled = false;
                RegularExpressionValidator3.Enabled = false;
                RegularExpressionValidator4.Enabled = false;
                RegularExpressionValidator5.Enabled = false;
            }

            else {

                RequiredFieldValidator1.Enabled = true;
                RequiredFieldValidator10.Enabled = true;
                RequiredFieldValidator2.Enabled = true;
                RequiredFieldValidator3.Enabled = true;
                RequiredFieldValidator4.Enabled = true;
                RequiredFieldValidator5.Enabled = true;
                RequiredFieldValidator6.Enabled = true;
                RequiredFieldValidator7.Enabled = true;
                RequiredFieldValidator8.Enabled = true;
                RequiredFieldValidator9.Enabled = true;
                RequiredFieldValidator11.Enabled = true;
                RegularExpressionValidator1.Enabled = true;
                RegularExpressionValidator3.Enabled = true;
                RegularExpressionValidator4.Enabled = true;
                RegularExpressionValidator5.Enabled = true;
            
            
            }
        }
 

        protected void Page_Load(object sender, EventArgs e)
        {
            this.ThisWebPart = this.Parent as NewClientForm;

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect(ThisWebPart.SearchClientUrl);
        }

        protected void btnCreateNewClient_Click(object sender, EventArgs e)
        {
            var c = Production.Business.Client.NewClient();
            c.Address = txtAddress.Text;
            c.Attention = txtAttention.Text;
            c.City = txtCity.Text;
            c.ClientId = txtClientID.Text;
            c.ClientName = txtName.Text;
            c.Country = txtCountry.Text;
            c.Email = txtEmail.Text;
            c.Fax = txtFax.Text;
            c.Phone = txtPhone.Text;
            c.State = txtState.Text;
            c.Zip = txtZip.Text;

            try
            {

                c.Save();

                var redirectUrl = Shared.Utilities.CreateClientWebSite(txtClientID.Text, txtClientID.Text);

                Response.Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message + "<hr/>";

                foreach (var item in c.BrokenRulesCollection)
                {
                    lblMessage.Text += item.Description + "<hr/>";
                }
      
            }

        }
    }
}
