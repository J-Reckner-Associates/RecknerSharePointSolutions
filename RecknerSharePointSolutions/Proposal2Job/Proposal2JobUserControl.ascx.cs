using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Deployment;
using System.Web;

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

                if (ThisWebPart.DestionationSiteCollectionUrl != null)
                {

                    oSite = new SPSite(ThisWebPart.DestionationSiteCollectionUrl + "/jobs/" + DateTime.Now.Year);

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {

                        populateJobList();


                    });
                }
                             
            }
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            if (ThisWebPart.DestionationSiteCollectionUrl != null)
            {

                oSite = new SPSite(ThisWebPart.DestionationSiteCollectionUrl + "/jobs/" + DateTime.Now.Year);

                SPSecurity.RunWithElevatedPrivileges(delegate
                {

                    using (SPWeb sourceWeb = new SPSite(SPContext.Current.Web.Url).OpenWeb())
                    {
                        SPExportSettings settings = new SPExportSettings();
                        settings.SiteUrl = sourceWeb.Site.Url;
                        settings.ExportMethod = SPExportMethodType.ExportAll;
                        settings.FileLocation = ThisWebPart.ExportLocation;
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

                SPWeb destinationWeb = new SPSite(lstJobs.SelectedItem.Value).OpenWeb();

                SPSecurity.RunWithElevatedPrivileges(delegate
                {

                    HttpContext.Current.Items["FormDigestValidated"] = "false";
                    destinationWeb.AllowUnsafeUpdates = true;


                    SPImportSettings settings = new SPImportSettings();
                    settings.SiteUrl = destinationWeb.Site.Url;
                    settings.WebUrl = lstJobs.SelectedItem.Value;
                    settings.FileLocation = ThisWebPart.ExportLocation;
                    settings.FileCompression = false;
                    settings.RetainObjectIdentity = false;
                    settings.LogFilePath = ThisWebPart.ExportLocation + @"\export_log.txt";
                    settings.IgnoreWebParts = true;

                    SPImport import = new SPImport(settings);

                    import.Run();
                    HttpContext.Current.Items["FormDigestValidated"] = "false";
                    destinationWeb.AllowUnsafeUpdates = false;

                });

                var currentUser = Request.LogonUserIdentity.ToString();


                SPSecurity.RunWithElevatedPrivileges(delegate
                {



                    SPWeb sourceWeb = new SPSite(SPContext.Current.Web.Url).OpenWeb();
                    sourceWeb.AllowUnsafeUpdates = true;

                    sourceWeb.Delete();

                    //TODO: Update proposal record here get with the ID then update.


                });

                Response.Redirect(destinationWeb.Url);
            }
        }
    }
}
