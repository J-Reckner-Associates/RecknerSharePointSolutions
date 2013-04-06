<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewClientFormUserControl.ascx.cs" Inherits="RecknerSharePointSolutions.NewClientForm.NewClientFormUserControl" %>


 <script type="text/javascript">
     $(document).ready(function () {
         $('.wait').one('click', function () {
             $(this).slideUp();
             $('[id$="_lblMessage"]').text('Please wait!');
         });
   
  </script>

  <table ID="newClientForm" runat="server"  >
            <tr>
                <td align="right">
                    ClientID:</td>
                <td>
                    <asp:TextBox ID="txtClientID" runat="server" MaxLength="6" 
                        ValidationGroup="createclientvalidation" CssClass="client-id"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtClientID" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                        ControlToValidate="txtClientID" ErrorMessage="RegularExpressionValidator" 
                        ValidationExpression="^[a-zA-Z0-9_]*$" ValidationGroup="createclientvalidation">Only alpha numeric characters please!</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" 
                        ValidationGroup="createclientvalidation" Width="220px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="txtName" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnCreateNewClient" runat="server" CssClass=".wait" 
                        onclick="btnCreateNewClient_Click" Text="Create" 
                        ValidationGroup="createclientvalidation" />
                    
                    &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
                    
                    </td>
            </tr>
            </table>
<asp:Label ID="lblMessage" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
