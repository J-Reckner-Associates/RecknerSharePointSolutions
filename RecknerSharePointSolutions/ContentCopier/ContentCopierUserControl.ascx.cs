using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Deployment;
using Microsoft.SharePoint.Administration;
using System.IO;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace RecknerSharePointSolutions.ContentCopier
{
    public partial class ContentCopierUserControl : UserControl
    {
        public ContentCopier ContentCopierWebPart { get; set; }

        public bool isValidUrl(string url)
        {
            if (url == null || url == "")
            { return false; }
            else
            {
                string pattern = @"^(http|https|ftp)\://[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
                Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return reg.IsMatch(url);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            this.ContentCopierWebPart = this.Parent as ContentCopier;
         
            if (!Page.IsPostBack)
            {

                lblCopy.Text = ContentCopierWebPart.CopyMessage;


                btnCopy.Enabled = Context.User.IsInRole(ContentCopierWebPart.AuthorizedRole);
              
             
                    string webUrl = ContentCopierWebPart.DestionationSiteCollectionUrl;

                    if (this.isValidUrl(webUrl))
                    {

                        using (SPWeb oWebsite = new SPSite(webUrl).OpenWeb())
                        {

                            if (oWebsite != null)
                            {
                                SPWebCollection collWebsite = oWebsite.Webs;

                                foreach (SPWeb subSite in collWebsite)
                                {
                                    lstJobs.Items.Add(new ListItem(subSite.Title, subSite.Url));
                                    subSite.Close();
                                }
                            }

                        }

                   
                }

            }
        }

   
        protected void btnCopy_Click(object sender, EventArgs e)
        {


            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                
                using (SPWeb sourceWeb = new SPSite(SPContext.Current.Web.Url).OpenWeb())
                {
                    SPExportSettings settings = new SPExportSettings();
                    settings.SiteUrl = sourceWeb.Site.Url;
                    settings.ExportMethod = SPExportMethodType.ExportAll;
                    settings.FileLocation = ContentCopierWebPart.ExportLocation;
                    settings.FileCompression = false;
                    settings.CommandLineVerbose = true;
                    settings.OverwriteExistingDataFile = true;
                    
                    foreach (SPList item in sourceWeb.Lists)
                    {
                        SPExportObject exportObject = new SPExportObject();
                        exportObject.Id = item.ID;
                        exportObject.Type = SPDeploymentObjectType.List;
                        settings.ExportObjects.Add(exportObject); 
                    }
      
                    SPExport export = new SPExport(settings);
             
                     export.Run(); 
 

                }


            });


               SPSecurity.RunWithElevatedPrivileges(delegate
                {
 
                    SPWeb destinationWeb = new SPSite(lstJobs.SelectedItem.Value).OpenWeb();
    
                        HttpContext.Current.Items["FormDigestValidated"] = "false";
                        destinationWeb.AllowUnsafeUpdates = true;
                         

                        SPImportSettings settings = new SPImportSettings();
                        settings.SiteUrl = destinationWeb.Site.Url;
                        settings.WebUrl = lstJobs.SelectedItem.Value;
                        settings.FileLocation = ContentCopierWebPart.ExportLocation;
                        settings.FileCompression = false; 
                        settings.RetainObjectIdentity = false;
                        settings.LogFilePath = ContentCopierWebPart.ExportLocation + @"\export_log.txt";
                        settings.IgnoreWebParts = true;
                         
                       
                        SPImport import = new SPImport(settings);

                      

                        import.Run();
                        HttpContext.Current.Items["FormDigestValidated"] = "false";
                        destinationWeb.AllowUnsafeUpdates = false;

                        Response.Redirect(destinationWeb.Url);
                              

            });



        }
    }
}
