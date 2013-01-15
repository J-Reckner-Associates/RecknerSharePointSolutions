using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using System.Web;

namespace RecknerSharePointSolutions.Blueberry_Contacts_Event_Receiver
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class Blueberry_Contacts_Event_Receiver : SPItemEventReceiver
    {
        HttpContext ctx = null;
        string ClientID = "";
    
        public Blueberry_Contacts_Event_Receiver()
        {
            ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Request.QueryString["ClientID"] != null)
                {
                    ClientID = ctx.Request.QueryString["ClientID"];
                   
                }
                                   
            }
        }

 
       /// <summary>
       /// An item was added
       /// </summary>
       public override void ItemAdded(SPItemEventProperties properties)
       {
           base.ItemAdded(properties);

           if (properties.ListTitle == "ClientContacts")
           {
                   properties.ListItem["ClientID"] = ClientID;
                   properties.ListItem.Update();
 
             

           }
       }


    }
}
