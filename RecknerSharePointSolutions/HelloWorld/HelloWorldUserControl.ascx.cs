using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace RecknerSharePointSolutions.HelloWorld
{
    public partial class HelloWorldUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "Hello " + Context.User.Identity.Name;
            
        }
    }
}
