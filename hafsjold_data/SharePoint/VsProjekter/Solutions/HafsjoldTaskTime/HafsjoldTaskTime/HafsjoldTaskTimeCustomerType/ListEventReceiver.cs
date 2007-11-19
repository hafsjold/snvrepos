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
    [Guid("f6806b09-18bb-4562-9738-9687a880543a")]
    public class HafsjoldTaskTimeCustomerTypeListEventReceiver : SPListEventReceiver
    {
        /// <summary>
        /// Initializes a new instance of the Microsoft.SharePoint.SPListEventReceiver class.
        /// </summary>
        public HafsjoldTaskTimeCustomerTypeListEventReceiver()
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
