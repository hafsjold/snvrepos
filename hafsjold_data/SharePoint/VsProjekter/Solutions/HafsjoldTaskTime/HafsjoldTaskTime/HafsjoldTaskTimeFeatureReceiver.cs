using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace HafsjoldTaskTimeFeatureReceiver
{
    [CLSCompliant(false)]
    [Guid("514173BF-4690-41f8-AE67-75081010C50C")]
    public class FeatureReceiver : SPFeatureReceiver
    {

        // no functionality required for install/uninstall events
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FeatureInstalled(SPFeatureReceiverProperties properties) { }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FeatureUninstalling(SPFeatureReceiverProperties properties) { }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb site = (SPWeb)properties.Feature.Parent;
            // track original site Title using SPWeb property bag
            site.Properties["OriginalTitle"] = site.Title;
            site.Properties.Update();
            // update site title
            site.Title = "Hello World Hafsjold";
            site.Update();
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            // reset site Title back to its original value
            SPWeb site = (SPWeb)properties.Feature.Parent;
            site.Title = site.Properties["OriginalTitle"];
            site.Update();
        }
    }
}
