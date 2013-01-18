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

    /* 
     * 1. Make the title column unique to prevent dupplicate site creation
     * 2. In the template SharePoint Server Publishing feature must be enabled. 
     */
    /// <summary>
    /// List Item Events
    /// </summary>
    public class Blueberry_Proposals_List_Event_Receiver : SPItemEventReceiver
    {
        HttpContext ctx = null;
        string ClientID = "";
        string ClientName = "";
        string TemplateName = "";
   
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

       void CreateProposalSite(SPWeb currentWeb, string ClientID, string ProposalID, string ProposalName, int ProposalRecordID)
       {
           if (ClientID != string.Empty && ProposalID != string.Empty)
           {

          
               SPSecurity.RunWithElevatedPrivileges(delegate()
               {

                   SPWeb clientWeb = currentWeb.Webs[ClientID];

                   SPSite oSite = currentWeb.Site;

                   TemplateName = oSite.RootWeb.Properties["ProposalSiteTemplateName"].ToString();

                   
                   SPWebTemplateCollection wtc = oSite.GetWebTemplates(1033);

                   SPWebTemplate wt = wtc[TemplateName];

                   //Creates a website with uniqueID
                   
                   SPWeb newProposalWeb = clientWeb.Webs.Add(ProposalID, ProposalName, ProposalName, 1033, wt, false, false);
          
                   newProposalWeb.Navigation.UseShared = true;

                   if (newProposalWeb.Properties.ContainsKey("ClientID"))
                   {

                       newProposalWeb.Properties["ClientID"] = ClientID;
                   }

                   else {

                       newProposalWeb.Properties.Add("ClientID", ClientID);
                   
                   }


                   if (newProposalWeb.Properties.ContainsKey("ClientName"))
                   {

                       newProposalWeb.Properties["ClientName"] = ClientName;
                   }

                   else {

                       newProposalWeb.Properties.Add("ClientName", ClientName);
                   
                   }

                   if (newProposalWeb.Properties.ContainsKey("ProposalID"))
                   {

                       newProposalWeb.Properties["ProposalID"] = ProposalID;
                   }
                   else {

                       newProposalWeb.Properties.Add("ProposalID", ProposalID);
                   
                   }


                   if (newProposalWeb.Properties.ContainsKey("ProposalRecordID"))
                   {

                       newProposalWeb.Properties["ProposalRecordID"] = ProposalRecordID.ToString();
                   }
                   else
                   {

                       newProposalWeb.Properties.Add("ProposalRecordID", ProposalRecordID.ToString());

                   }



                 

                   newProposalWeb.Properties.Update();
                   
                   //sets the default page.
                   SPFile f = newProposalWeb.GetFile("Pages/Home.aspx");

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
         //  base.ItemAdded(properties);
            
           var currentWeb = properties.OpenWeb();
                 
     
           if (properties.ListTitle == "Proposals")
           {

              string ProposalID = RemoveNonAlphaAndSpaces(properties.ListItem["Title"].ToString());
              string  ProposalName = properties.ListItem["Title"].ToString();

     
              properties.ListItem["ClientID"] = ClientID;
              properties.ListItem["ProposalID"] = ProposalID;
              properties.ListItem.Update();

              this.CreateProposalSite(currentWeb, ClientID, ProposalID, ProposalName, properties.ListItem.ID);

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
 