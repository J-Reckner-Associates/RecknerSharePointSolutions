using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Publishing;


namespace RecknerSharePointSolutions.Shared
{
   public static class Utilities
    {
       public static string CreateClientWebSite(string ClientID, string ClientName)
       {
           var redirectUrl = "";

           SPSecurity.RunWithElevatedPrivileges(delegate()
           {
               SPWeb currentWeb = SPContext.Current.Web;

              
               SPSite oSite = SPContext.Current.Site;

               var siteTemplateName = oSite.RootWeb.Properties["ClientSiteTemplateName"].ToString();

               SPWebTemplateCollection wtc = oSite.GetWebTemplates(1033);

               SPWebTemplate wt = wtc[siteTemplateName];

               //Creates a website with uniqueID
               SPWeb newWeb = currentWeb.Webs.Add(ClientID, ClientName, ClientName, 1033, wt, false, false);

               if (newWeb.Properties.ContainsKey("ClientID"))
               {

                   newWeb.Properties["ClientID"] = ClientID;
               }

               else {

                   newWeb.Properties.Add("ClientID", ClientID);
               }

               if (newWeb.Properties.ContainsKey("ClientName"))
               {

                   newWeb.Properties["ClientName"] = ClientID;
               }

               else
               {

                   newWeb.Properties.Add("ClientName", ClientID);
               }

                
              
                newWeb.Properties.Update();

               newWeb.Navigation.UseShared = true;

               //Updates default page.
               SPFile f = newWeb.GetFile("pages/Home.aspx");

               newWeb.Update();
               PublishingWeb w = PublishingWeb.GetPublishingWeb(newWeb);
               w.DefaultPage = f;
               w.Update();

               newWeb.Close();
               w.Close();

               redirectUrl = newWeb.Url;
              

           });

           return redirectUrl;

       }

       public static void DeleteWebSite(string ClientID, string ProposalID) {

           if (ClientID != string.Empty && ProposalID != string.Empty)
           {
 
               SPWeb currentWeb = SPContext.Current.Web;

               SPSecurity.RunWithElevatedPrivileges(delegate()
               {
                   currentWeb.Webs.Delete(ClientID + "/" + ProposalID);
               });


           }
       
       }


    }
}
