using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using SPDevtools;

namespace HafsjoldTaskTimeProjectTimeType
{
    [CLSCompliant(false)]
    [TargetContentType("f7934709-cd20-4e5a-a56d-06e2b6451ef4", "0x0100d5619968e71c4b439a30a015f3cb43ce")]
    [Guid("9ad75b82-b2fc-4be3-9e4b-3690ee1e3307")]
    public class HafsjoldTaskTimeProjectTimeTypeItemEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Initializes a new instance of the Microsoft.SharePoint.SPItemEventReceiver class.
        /// </summary>
        public HafsjoldTaskTimeProjectTimeTypeItemEventReceiver()
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel=true)]
        public override void ContextEvent(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after a new item has been added to its containing object.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAdded(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs when a new item is added to its containing object.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAdding(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after a user adds an attachment to an item.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentAdded(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs when a user adds an attachment to an item.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentAdding(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs when after a user removes an attachment from an item.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentDeleted(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs when a user removes an attachment from an item.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemAttachmentDeleting(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after an item is checked in.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckedIn(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after an item is checked out.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckedOut(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs as a file is being checked in.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckingIn(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs after an item is checked out.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemCheckingOut(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after an existing item is completely deleted.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemDeleted(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs before an existing item is completely deleted.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemDeleting(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="properties">
        /// TODO
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileConverted(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs after a file is moved.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileMoved(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Occurs when a file is being moved.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemFileMoving(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs as an item is being unchecked out.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUncheckedOut(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs as an item is being unchecked out.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUncheckingOut(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Asynchronous after event that occurs after an existing item is changed, for example, when the user changes data in one or more fields.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUpdated(SPItemEventProperties properties)
        {
        }

        /// <summary>
        /// Synchronous before event that occurs when an existing item is changed, for example, when the user changes data in one or more fields.
        /// </summary>
        /// <param name="properties">
        /// A Microsoft.SharePoint.SPItemEventProperties object that represents properties of the event handler.
        /// </param>
        [SharePointPermission(SecurityAction.LinkDemand, ObjectModel = true)]
        public override void ItemUpdating(SPItemEventProperties properties)
        {
        }
    }
}
