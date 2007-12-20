<%@ Control Language="C#" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>

<script runat="server">
  protected override void OnLoad(EventArgs e)  {
    SPWeb site = SPContext.Current.Web;
    lblDisplay.Text = "Current Site: " + site.Url;
  }
</script>

<asp:Label ID="lblDisplay" runat="server" />

