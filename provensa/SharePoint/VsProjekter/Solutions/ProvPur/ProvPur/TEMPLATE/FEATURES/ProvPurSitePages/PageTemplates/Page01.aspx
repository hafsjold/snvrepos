<%@ Register TagPrefix="luc" TagName="UserControl1" src="~/_controltemplates/ProvPur/UserControl1.ascx" %>
<%@ Register TagPrefix="luc" TagName="UserControl2" src="~/_controltemplates/ProvPur/UserControl2.ascx" %>
<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" meta:progid="SharePoint.WebPartPage.Document"  %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PlaceHolderMain">
  
  <h3>Page 1 - User Control Demo</h3>
  
  <luc:UserControl1 ID="id1" runat="server" />
  
  <hr />
  
  <luc:UserControl2 ID="id2" runat="server" />
  
</asp:Content>
