<%@ Control Language="C#" %>

<script runat="server">

  protected void cmdAddCustomer_Click(object sender, EventArgs e) {
    string msg  = "Customer " + txtName.Text + " has been added.";
    lblStatus.Text = msg;
  }
  
</script>


<h4>Add New Customer</h4>
<table>
  <tr>
    <td class="ms-formlabel">Name:</td>
    <td Class="ms-formbody"><asp:TextBox ID="txtName" runat="server" /></td>
  </tr>
  <tr>
    <td class="ms-formlabel">Address:</td>
    <td Class="ms-formbody"><asp:TextBox ID="txtAddress" runat="server" /></td>
  </tr>
  <tr>
    <td class="ms-formlabel">City:</td>
    <td Class="ms-formbody"><asp:TextBox ID="txtCity" runat="server" /></td>
  </tr>
  <tr>
    <td class="ms-formlabel">State:</td>
    <td Class="ms-formbody"><asp:TextBox ID="txtState" runat="server" /></td>
  </tr>
  <tr>
    <td class="ms-formlabel">Zip:</td>
    <td Class="ms-formbody"><asp:TextBox ID="txtZip" runat="server" /></td>
  </tr>

</table>

<p>
  <asp:Button ID="cmdAddCustomer" runat="server" Text="Add Customer" OnClick="cmdAddCustomer_Click" CssClass="ms-buttonheightwidth" />
</p>
<p>
  <asp:Label ID="lblStatus" runat="server" Text="" />
</p>

