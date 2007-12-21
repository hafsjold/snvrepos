<%@ Page MasterPageFile="~masterurl/default.master" 
    meta:progid="SharePoint.WebPartPage.Document" %>

<%@ Register TagPrefix="luc" TagName="FileViewer" 
    src="~/_controltemplates/ProvPur/FileViewer.ascx" %>

<asp:Content ID="main" runat="server" ContentPlaceHolderID="PlaceHolderMain">
  
  <h3>Page 2 - Site File Viewer</h3>
  
  <luc:FileViewer ID="id1" runat="server" /> 
  
</asp:Content>
