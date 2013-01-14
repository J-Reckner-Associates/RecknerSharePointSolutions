<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchClientUserControl.ascx.cs" Inherits="RecknerSharePointSolutions.SearchClient.SearchClientUserControl" %>

 <script type="text/javascript">

     function refreshPanel() {
         __doPostBack('<%= txtClientNameSearch.ClientID %>', '');

     }

 
  </script>

  <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server">
</asp:ScriptManagerProxy>

 
        <asp:Label ID="Label2"  runat="server" Text="Client Name:"></asp:Label>    <asp:TextBox   ID="txtClientNameSearch" runat="server" Width="230px" 
         ontextchanged="txtClientNameSearch_TextChanged"  OnKeyUp ="refreshPanel();" ></asp:TextBox>
  
 

  
        <asp:UpdatePanel ID="UpdatePanel1"  runat="server"  >
            <ContentTemplate>
            

          <asp:Label ID="lblMessage" runat="server" EnableViewState="False" 
    Font-Bold="True" Font-Size="Large" ForeColor="Red" CssClass="wait"></asp:Label>

   
                 &nbsp;<asp:Button ID="btnNewClient" runat="server" Text="New Client" 
            Visible="False" onclick="btnNewClient_Click" />

   
                 <asp:GridView  ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataKeyNames="ClientID" EnableModelValidation="True" 
                    ForeColor="#333333" onrowcommand="GridView1_RowCommand" 
                    onrowdatabound="GridView1_RowDataBound" Width="50%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="ClientID" HeaderText="ClientID" />
                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                        <asp:TemplateField HeaderText="Create">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnCreateInSharePoint" runat="server" CssClass="wait">Create In SharePoint</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#FFFFCC" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
           </ContentTemplate>

             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="txtClientNameSearch" 
                    EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>


