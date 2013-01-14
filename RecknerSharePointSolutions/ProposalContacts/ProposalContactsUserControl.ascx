<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProposalContactsUserControl.ascx.cs" Inherits="RecknerSharePointSolutions.ProposalContacts.ProposalContactsUserControl" %>
<p style="float: right">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="assigncontacts.aspx" 
        Target="_self">[Assign a Contact]</asp:HyperLink>
    <br />
</p>
<asp:GridView ID="GridView1" runat="server" CellPadding="4" DataKeyNames="Id" 
    EnableModelValidation="True" ForeColor="#333333" 
    onrowcommand="GridView1_RowCommand" 
    onrowdatabound="GridView1_RowDataBound" AutoGenerateColumns="False" 
    Width="100%" GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Remove">
            <ItemTemplate>
                <asp:LinkButton ID="btnRemove" runat="server" CausesValidation="false" 
                    CommandName="" Text="Remove"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="FullName" HeaderText="Full Name" />
        <asp:BoundField DataField="Phone" HeaderText="Phone" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
