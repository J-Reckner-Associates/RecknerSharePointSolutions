using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using Microsoft.SharePoint;

namespace RecknerSharePointSolutions.HelloWorld
{
    public partial class HelloWorldUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Hello " + Context.User.Identity.Name;
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //SPSecurity.RunWithElevatedPrivileges(delegate()
            //{
               

            var cns = System.Configuration.ConfigurationManager.ConnectionStrings["hcalboru"].ToString();

            using (var cn = new SqlConnection(cns)) {
                
                cn.Open();
                var cmd = new SqlCommand("select top 200 * from  map ", cn);
                GridView1.DataSource = cmd.ExecuteReader();
                GridView1.DataBind();
                
            };

                
            //});
   
        }
    }
}
