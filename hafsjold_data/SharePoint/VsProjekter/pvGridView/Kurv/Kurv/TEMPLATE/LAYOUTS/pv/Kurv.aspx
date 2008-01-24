<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="Kurv, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>
<%@ Assembly Name="spListDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=9f4da00116c38ec5" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" MasterPageFile="~/_layouts/application.master" Inherits="CustomApplicationPages.Kurv"
    EnableViewState="true" EnableViewStateMac="true" %>

<%@ Import Namespace="spListDB"  %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="spListDB.spListDB" DataObjectTypeName=spListDB.clsData SelectMethod="GetLists" >
    </asp:ObjectDataSource>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
        </Columns>
    </asp:GridView>
    
</asp:Content>
