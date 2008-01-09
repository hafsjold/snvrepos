using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace CustomApplicationPages {

  public class Kurv : LayoutsPageBase {

    // add control fields to match controls tags on .aspx page
    protected ObjectDataSource ObjectDataSource1;
    protected GridView GridView1;

    protected override void OnLoad(EventArgs e) {
      
    }
   }
}
