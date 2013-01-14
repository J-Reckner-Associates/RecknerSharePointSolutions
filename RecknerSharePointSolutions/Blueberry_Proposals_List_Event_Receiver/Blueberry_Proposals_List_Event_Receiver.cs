using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.Publishing;
using System.Linq;

 
namespace RecknerSharePointSolutions.Blueberry_Proposals_List_Event_Receiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class Blueberry_Proposals_List_Event_Receiver : SPItemEventReceiver
    {
        HttpContext ctx = null;
        string ClientID = "";
        string ClientName = "";
        string TemplateName = "ENTERWIKI#0";
 
    
   
        public Blueberry_Proposals_List_Event_Receiver() : base()
        {
            ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Request.QueryString["ClientID"] != null)
                {
                    ClientID = ctx.Request.QueryString["ClientID"];
                    ClientName = ctx.Request.QueryString["ClientName"];

                }

                if (ctx.Request.QueryString["ClientName"] != null)
                {
                   
                    ClientName = ctx.Request.QueryString["ClientName"];

                }

                if (ctx.Request.QueryString["TemplateName"] != null)
                {
                  
                    TemplateName = ctx.Request.QueryString["TemplateName"];

                }
            }
            
        }

   
       string RemoveNonAlphaAndSpaces(string source)
       {

           return Regex.Replace(source, @"\W|_", string.Empty);
       }

       void CreateProposalSite(SPWeb currentWeb, string ClientID, string ProposalID, string ProposalName, string siteTemplateName)
       {
           if (ClientID != string.Empty && ProposalID != string.Empty)
           {

               SPSecurity.RunWithElevatedPrivileges(delegate()
               {

                   SPWeb clientWeb = currentWeb.Webs[ClientID];

                   SPSite oSite = currentWeb.Site;

                   SPWebTemplateCollection wtc = oSite.GetWebTemplates(1033);

                   SPWebTemplate wt = wtc[siteTemplateName];

                   //Creates a website with uniqueID


                   SPWeb newProposalWeb = clientWeb.Webs.Add(ProposalID, ProposalName, ProposalName, 1033, wt, false, false);

                   newProposalWeb.Properties.Add("ClientID", ClientID);
                   newProposalWeb.Properties.Add("ClientName", ClientName);
                   newProposalWeb.Properties.Add("ProposalID", ProposalID);
                   newProposalWeb.Properties.Update();

                   newProposalWeb.Navigation.UseShared = true;

                   //Updates default page.
                   SPFile f = newProposalWeb.GetFile("pages/Default.aspx");

                   newProposalWeb.Update();
                   PublishingWeb w = PublishingWeb.GetPublishingWeb(newProposalWeb);
                   w.DefaultPage = f;
                   w.Update();

                   newProposalWeb.Close();
                   w.Close();

 
               });

           }
       }

       /// <summary>
       /// An item is being deleted
       /// </summary>
       public override void ItemDeleting(SPItemEventProperties properties)
       {
           base.ItemDeleting(properties);

           if (properties.ListTitle == "Proposals")
           {
               if (properties.ListItem["ClientID"] != null && properties.ListItem["ProposalID"] != null) {

                   var deletingClientID = properties.ListItem["ClientID"].ToString();
                   var deletingProposalID = properties.ListItem["ProposalID"].ToString();

                              

                   if (deletingClientID != string.Empty && deletingProposalID != string.Empty)
                   {

                       SPSecurity.RunWithElevatedPrivileges(delegate()
                       {
                           var currentWeb = properties.OpenWeb();

                           SPWeb clientWeb = currentWeb.Webs[deletingClientID];

                           var deletingWeb = clientWeb.Webs[deletingProposalID];

                           if (deletingWeb.Exists)
                           {

                               clientWeb.Webs.Delete(deletingProposalID);

                           }
                           

                       });

                   }
               
               }
      

           }

       }

 
       /// <summary>
       /// An item was added
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           base.ItemAdded(properties);

      
           var currentWeb = properties.OpenWeb();
                  

           if (properties.ListTitle == "Proposals")
           {

              string ProposalID = RemoveNonAlphaAndSpaces(properties.ListItem["Title"].ToString());
              string  ProposalName = properties.ListItem["Title"].ToString();
                
    
               properties.ListItem["ClientID"] = ClientID;
               properties.ListItem["ProposalID"] = ProposalID;
               properties.ListItem.Update();

               var webCount = 0;

               var clientWeb = currentWeb.Webs[ClientID];

               if (clientWeb.Exists)
               {

                   var tmp = ProposalID.Substring(0, ProposalID.Length - 2);

                   if (ClientID != string.Empty && ProposalID != string.Empty)
                   {

                       foreach (SPWeb item in clientWeb.Webs)
                       {
                           if (item.Name.ToLower().Contains(tmp))
                           {
                               webCount += 1;
                           }

                       }

                       if (webCount > 0)
                       {

                           ProposalID += webCount.ToString();

                           properties.ListItem["ProposalID"] = ProposalID;
                           properties.ListItem["Title"] = properties.ListItem["Title"].ToString() + " " + webCount.ToString();
                           ProposalName += " " + webCount.ToString();
                           properties.ListItem.Update();

                       }

                        
                       this.CreateProposalSite(currentWeb, ClientID, ProposalID, ProposalName, TemplateName);


                   }
               }
                
           }
       }

    

       /// <summary>
       /// An item is being updated
       /// </summary>
       public override void ItemUpdating(SPItemEventProperties properties)
       {
           base.ItemUpdating(properties);


           if (properties.ListTitle == "Proposals")
           {
                              
               if (properties.ListItem["ClientID"] != null && properties.ListItem["ProposalID"] != null)
               {
                    string afterValue = "";
                    string afterValueID = "";

                   var updatingClientID = properties.ListItem["ClientID"].ToString();
                   var ProposalName = properties.ListItem["Title"].ToString();

                   if (properties.AfterProperties["Title"] != null) {
                   
                    afterValue =   properties.AfterProperties["Title"].ToString();
                    afterValueID = RemoveNonAlphaAndSpaces(afterValue);
                                    


                   var updatingProposalID = RemoveNonAlphaAndSpaces(ProposalName);

                 
                   if (updatingClientID != string.Empty && updatingProposalID != string.Empty)
                   {
                       if (afterValueID != string.Empty)
                       {
                           SPSecurity.RunWithElevatedPrivileges(delegate()
                           {
                               var currentWeb = properties.OpenWeb();

                               SPWeb clientWeb = currentWeb.Webs[updatingClientID];

                               var updatingWeb = clientWeb.Webs[updatingProposalID];

                               if (updatingWeb.Exists)
                               {

                                   updatingWeb.Name = afterValueID;
                                   updatingWeb.Title = afterValue;
                                   updatingWeb.Update();



                               }


                           });

                       }

                   }
                   }

               }
           }


       }

       /// <summary>
       /// An item was updated
       /// </summary>
       public override void ItemUpdated(SPItemEventProperties properties)
       {
           base.ItemUpdated(properties);

           if (properties.ListTitle == "Proposals")
           {

               properties.ListItem["ProposalID"] = RemoveNonAlphaAndSpaces(properties.ListItem["Title"].ToString());
               properties.ListItem.Update();

           }

       }
    }
}
 