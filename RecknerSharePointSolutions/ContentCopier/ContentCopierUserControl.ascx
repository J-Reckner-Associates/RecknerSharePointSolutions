<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentCopierUserControl.ascx.cs" Inherits="RecknerSharePointSolutions.ContentCopier.ContentCopierUserControl" %>
 
 
 <script type="text/javascript">
     $(document).ready(function () {
         $('.copy').one('click', function () {
             $(this).slideUp();
             $('[id$="_lblMessage"]').text('Please wait!');

         });

 
     });
  </script>
 

   <table>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblCopy" runat="server" Font-Size="Medium"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Job#"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="lstJobs" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td align="right">
            <asp:Button ID="btnCopy" CssClass="copy" runat="server" onclick="btnCopy_Click" 
                Text="Copy" />
        </td>
    </tr>
</table>

<asp:Label ID="lblMessage" runat="server" EnableViewState="False" 
    Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>


