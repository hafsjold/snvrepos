using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace #NS#
{
    [CLSCompliant(false)]
    public class #CLASS# : SPItemEventReceiver
    {

        public #CLASS#()
        {
        }

 
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel=true)]
        public override void ContextEvent(SPItemEventProperties properties)
        {
        }

 
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAdded(SPItemEventProperties properties)
        {
        }

 
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAdding(SPItemEventProperties properties)
        {
        }

         [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentAdded(SPItemEventProperties properties)
        {
        }

         [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentAdding(SPItemEventProperties properties)
        {
        }

         [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentDeleted(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentDeleting(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckedIn(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckedOut(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckingIn(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckingOut(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemDeleted(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemDeleting(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileConverted(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileMoved(SPItemEventProperties properties)
        {
        }

         [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileMoving(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUncheckedOut(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUncheckingOut(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUpdated(SPItemEventProperties properties)
        {
        }

        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUpdating(SPItemEventProperties properties)
        {
        }
    }
}
