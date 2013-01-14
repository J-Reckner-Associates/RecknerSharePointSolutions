using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;
using Microsoft.SharePoint;


namespace RecknerSharePointSolutions.BlueBerryJobsByClient1
{
    public partial class BlueBerryJobsByClient1UserControl : UserControl
    {
        public BlueBerryJobsByClient1 ThisWebPart { get; set; }

        public string LinkToJobSite { get; set; }

        int jobYear = 0;

        String q =
        "select  substring(convert(nvarchar, JobNum), 5 ,5) as JobNum, substring(convert(nvarchar, JobNum), 5 ,5)  + ' ' + JobName as JobNumberWithName from JobLog where DATEPART (year , Started) >= @JobYear and ClientID = @ClientID order by JobNum DESC";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.ThisWebPart = this.Parent as BlueBerryJobsByClient1;

            this.LinkToJobSite = ThisWebPart.JobSiteUrl + @"/" + ThisWebPart.JobYear + @"/";

            var Connnection = System.Configuration.ConfigurationManager.ConnectionStrings["proback"].ToString();
    
            if (!IsPostBack)
            {

                SPWeb currentWeb = SPContext.Current.Web;

                if (currentWeb.Properties.ContainsKey("ClientName")) {

                    lblClientName.Text = currentWeb.Properties["ClientName"].ToString();
                                    
                }

                if (ThisWebPart.JobYear == 0)
                {
                    jobYear = DateTime.Now.Year - 1;
                 
                }

                lblYear.Text = jobYear + " +1";

                if (ThisWebPart.SiteUrl != string.Empty && ThisWebPart.JobSiteUrl != string.Empty)
                {
 
                    var cn = new SqlConnection();
                    cn.ConnectionString = Connnection;
                    var cmd = new SqlCommand(q, cn);
                    cmd.Parameters.Add("JobYear", SqlDbType.Int);
                    cmd.Parameters.Add("ClientID", SqlDbType.NVarChar);
                    cmd.Parameters[0].Value = jobYear;
                    cmd.Parameters[1].Value = currentWeb.Properties["ClientID"].ToString();
                    cn.Open();

                    try
                    {
                        DataList1.DataSource = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        DataList1.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                    }

                }

                else {

                    lblMessage.Text = "Webpart properties have not bee set jobs for this client will not display!";
                }
            }

        }
    }
}
