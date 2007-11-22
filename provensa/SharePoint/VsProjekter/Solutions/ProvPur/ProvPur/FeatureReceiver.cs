using System;
using Microsoft.SharePoint;

namespace ProvPur
{
    public class FeatureReceiver : SPFeatureReceiver
    {

        // no functionality required for install/uninstall events
        public override void FeatureInstalled(SPFeatureReceiverProperties properties) { }
        public override void FeatureUninstalling(SPFeatureReceiverProperties properties) { }

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb site = (SPWeb)properties.Feature.Parent;
            // track original site Title using SPWeb property bag
            site.Properties["OriginalTitle"] = site.Title;
            site.Properties.Update();
            // update site title
            site.Title = "Hello World";
            site.Update();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            // reset site Title back to its original value
            SPWeb site = (SPWeb)properties.Feature.Parent;
            site.Title = site.Properties["OriginalTitle"];
            site.Update();
        }
    }
}
