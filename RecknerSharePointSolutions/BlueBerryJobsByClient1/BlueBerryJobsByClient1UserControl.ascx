<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BlueBerryJobsByClient1UserControl.ascx.cs" Inherits="RecknerSharePointSolutions.BlueBerryJobsByClient1.BlueBerryJobsByClient1UserControl" %>
 

<asp:Label ID="Label2" runat="server" Text="Client :" Font-Bold="True" 
    Font-Size="Small" ForeColor="Red"></asp:Label>
 

<asp:Label ID="lblClientName" runat="server" Font-Bold="True" Font-Size="Small" 
    ForeColor="Red"></asp:Label>
<br />

<asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" 
    onitemdatabound="DataList1_ItemDataBound">
    <AlternatingItemStyle BackColor="White" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <ItemStyle BackColor="#EFF3FB" />
    <ItemTemplate>
        <asp:HyperLink ID="HyperLinkToJobSite" runat="server"></asp:HyperLink>
        </ItemTemplate>
    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
</asp:DataList>
<asp:Label ID="lblMessage" runat="server" Font-Size="Larger" ForeColor="Red"></asp:Label>

