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
                    Address :</td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" Height="54px" 
                        ValidationGroup="createclientvalidation" Width="220px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtAddress" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Attention :</td>
                <td>
                    <asp:TextBox ID="txtAttention" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ControlToValidate="txtAttention" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    City:
                </td>
                <td>
                    <asp:TextBox ID="txtCity" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="txtCity" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Zip:</td>
                <td>
                    <asp:TextBox ID="txtZip" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="txtCity" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtZip" ErrorMessage="RegularExpressionValidator" 
                        ValidationExpression="\d{5}(-\d{4})?" ValidationGroup="createclientvalidation">Invalid Zip Code</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    State:</td>
                <td>
                    <asp:TextBox ID="txtState" runat="server" MaxLength="2" 
                        ValidationGroup="createclientvalidation" Width="35px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="txtState" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Phone:</td>
                <td>
                    (xxx-xxx-xxxx)<br />
                    <asp:TextBox ID="txtPhone" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtPhone" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtPhone" ErrorMessage="RegularExpressionValidator" 
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" 
                        ValidationGroup="createclientvalidation">Invalid Phone Number</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Fax:
                </td>
                <td>
                    (xxx-xxx-xxxx)<br />
                    <asp:TextBox ID="txtFax" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="txtFax" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                        ControlToValidate="txtFax" ErrorMessage="RegularExpressionValidator" 
                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" 
                        ValidationGroup="createclientvalidation">Invalid Fax Number</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Email:</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" 
                        ValidationGroup="createclientvalidation"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="RequiredFieldValidator" 
                        ValidationGroup="createclientvalidation">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="createclientvalidation">Invalid Email</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Country:</td>
                <td>
                    <asp:TextBox ID="txtCountry" runat="server" 
                        ValidationGroup="createclientvalidation">USA</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                        ControlToValidate="txtCountry" ErrorMessage="RequiredFieldValidator" 
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
                    
                    </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;</td>
                <td align="right">
                    <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" />
                    
                    </td>
            </tr>
        </table>
<asp:Label ID="lblMessage" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
