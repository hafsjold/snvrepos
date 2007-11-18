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
    [Guid("5b1b93d3-663d-432a-81e9-941c3cd2663f")]
    public class HafsjoldTaskTimeProjectTimeTypeListEventReceiver : SPListEventReceiver
    {
        /// <summary>
        /// Initializes a new instance of the Microsoft.SharePoint.SPListEventReceiver class.
        /// </summary>
        public HafsjoldTaskTimeProjectTimeTypeListEventReceiver()
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
