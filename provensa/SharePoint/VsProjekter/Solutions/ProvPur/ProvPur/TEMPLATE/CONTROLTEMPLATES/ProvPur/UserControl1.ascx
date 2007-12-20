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
    <td>Name:</td>
    <td><asp:TextBox ID="txtName" runat="server" /></td>
  </tr>
  <tr>
    <td>Address:</td>
    <td><asp:TextBox ID="txtAddress" runat="server" /></td>
  </tr>
  <tr>
    <td>City:</td>
    <td><asp:TextBox ID="txtCity" runat="server" /></td>
  </tr>
  <tr>
    <td>
      State:
    </td>
    <td>
      <asp:TextBox ID="txtState" runat="server" />
    </td>
  </tr>

  <tr>
    <td>
      Zip:
    </td>
    <td>
      <asp:TextBox ID="txtZip" runat="server" />
    </td>
  </tr>


</table>

<p>
  <asp:Button ID="cmdAddCustomer" runat="server" Text="Add Customer" OnClick="cmdAddCustomer_Click" />
</p>
<p>
  <asp:Label ID="lblStatus" runat="server" Text="" />
</p>

