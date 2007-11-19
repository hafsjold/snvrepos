using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace CreateLookupField
{
    class Program
    {
        static void Main(string[] args)
        {
            SPSite site = new SPSite("http://localhost");
            SPWeb web = site.OpenWeb();

            // Get the Lookup List from the web for lookups
            SPList lookupList = web.Lists["HafsjoldTaskTimeCustomerList"];

            // Get the Dependent List from the web
            SPList dependentList = web.Lists["Hafsjold TaskTime ProjectTime List"];

            // Add a new lookup field to the Employee list called Departement
            // that will use the Department list for it's values
            dependentList.Fields.AddLookup("Department", lookupList.ID, false);

            // Cleanup and dispose of the web and site
            web.Dispose();
            site.Dispose();
        }
    }
}