using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using SPDevtools;

namespace HafsjoldLists
{
    [CLSCompliant(false)]
    [TargetList("2e3b4f69-aafc-4ee9-9306-591ed55790c4")]
    [Guid("0f050816-faab-40f4-934e-dd169f53ff62")]
    public class HafsjoldListsListEventReceiver : SPListEventReceiver
    {
        /// <summary>
        /// Initializes a new instance of the Microsoft.SharePoint.SPListEventReceiver class.
        /// </summary>
        public HafsjoldListsListEventReceiver()
        {
        }

        /// <summary>
        /// Occurs after a field link is added.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldAdded(SPListEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs when a fieldlink is being added to a content type.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldAdding(SPListEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs after a field has been removed from the list.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldDeleted(SPListEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs when a field is in process of being removed from the list.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldDeleting(SPListEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs after a field link has been updated
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldUpdated(SPListEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs when a fieldlink is being updated
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPListEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void FieldUpdating(SPListEventProperties properties)
        {
        }
    }
}
