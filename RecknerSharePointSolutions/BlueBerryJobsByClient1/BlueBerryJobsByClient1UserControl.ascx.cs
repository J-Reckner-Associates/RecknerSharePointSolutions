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

        

        int jobYear = 0;

        string clientName = "";
        string clientID = "";


        String q =
        "select datepart(year, started) as jobYear,  substring(convert(nvarchar, JobNum), 5 ,5) as JobNumOnly,   cast(jobNum as nvarchar)  + ' ' + JobName as JobNumberWithName from JobLog where DATEPART (year , Started) >= @JobYear and lower(ClientID) = @ClientID order by JobNum DESC";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            this.ThisWebPart = this.Parent as BlueBerryJobsByClient1;

            var Connnection = System.Configuration.ConfigurationManager.ConnectionStrings["proback"].ToString();
    
            if (!IsPostBack)
            {

                SPWeb currentWeb = SPContext.Current.Web;

                if (currentWeb.Properties.ContainsKey("ClientName")) {
                    
                    clientName = currentWeb.Properties["ClientName"].ToString();
                    lblClientName.Text = clientName;
              
                }

                if (currentWeb.Properties.ContainsKey("ClientID"))
                {

                    clientID = currentWeb.Properties["ClientID"].ToString();
                 
                }
                 
               
                    jobYear = DateTime.Now.Year - 1;
                               

                if (ThisWebPart.JobSiteUrl != string.Empty)
                {
 
                    var cn = new SqlConnection();
                    cn.ConnectionString = Connnection;
                    var cmd = new SqlCommand(q, cn);
                    cmd.Parameters.Add("JobYear", SqlDbType.Int);
                    cmd.Parameters.Add("ClientID", SqlDbType.NVarChar, 50);
                    cmd.Parameters[0].Value = jobYear;
                    cmd.Parameters[1].Value = clientID.ToLower();
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

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
          
                HyperLink linkToJobSite =  (HyperLink)e.Item.FindControl("HyperLinkToJobSite");

               if (linkToJobSite != null) {

                linkToJobSite.NavigateUrl = ThisWebPart.JobSiteUrl + "/" + DataBinder.Eval(e.Item.DataItem, "jobYear").ToString() + "/" + DataBinder.Eval(e.Item.DataItem, "jobNumOnly").ToString();
                linkToJobSite.Text = DataBinder.Eval(e.Item.DataItem, "jobNumberWithName").ToString();
               }
            
            
        }
    }
}
