using System;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using SPDevtools;

namespace HafsjoldTaskTimeCustomerType
{
    [CLSCompliant(false)]
    [TargetContentType("a47fba7f-89f6-448f-9f6a-aaed70cdc494", "0x0100317218c260054e1ba1af8aa1b0d67822")]
    [Guid("37fcac90-7f4b-4c43-87ea-6870a278f392")]
    public class HafsjoldTaskTimeCustomerTypeItemEventReceiver : SPItemEventReceiver
    {
        /// <summary>
        /// Initializes a new instance of the Microsoft.SharePoint.SPItemEventReceiver class.
        /// </summary>
        public HafsjoldTaskTimeCustomerTypeItemEventReceiver()
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
