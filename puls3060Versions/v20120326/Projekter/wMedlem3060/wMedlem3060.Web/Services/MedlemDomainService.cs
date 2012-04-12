
namespace wMedlem3060.Web.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using wMedlem3060.Web;


    // Implements application logic using the MedlemEntities context.
    // TODO: Add your application logic to these methods or in additional methods.
    // TODO: Wire up authentication (Windows/ASP.NET Forms) and uncomment the following to disable anonymous access
    // Also consider adding roles to restrict access as appropriate.
    [RequiresAuthentication]
    [EnableClientAccess()]
    public class MedlemDomainService : LinqToEntitiesDomainService<MedlemEntities>
    {

        // TODO:
        // Consider constraining the results of your query method.  If you need additional input you can
        // add parameters to this method or create additional query methods with different names.
        // To support paging you will need to add ordering to the 'tblMedlems' query.
        public IQueryable<tblMedlem> GetTblMedlems()
        {
            return this.ObjectContext.tblMedlems.OrderBy(e=>e.Nr);
        }
        
        public IQueryable<tblMedlem> GetYoungTblMedlems()
        {
            return this.ObjectContext.tblMedlems.Where(e => e.FodtDato < new DateTime(1982,01,01)).OrderBy(e => e.Nr);
        }
        
        public void InsertTblMedlem(tblMedlem tblMedlem)
        {
            if ((tblMedlem.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tblMedlem, EntityState.Added);
            }
            else
            {
                this.ObjectContext.tblMedlems.AddObject(tblMedlem);
            }
        }

        public void UpdateTblMedlem(tblMedlem currenttblMedlem)
        {
            this.ObjectContext.tblMedlems.AttachAsModified(currenttblMedlem, this.ChangeSet.GetOriginal(currenttblMedlem));
        }

        public void DeleteTblMedlem(tblMedlem tblMedlem)
        {
            if ((tblMedlem.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tblMedlem, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.tblMedlems.Attach(tblMedlem);
                this.ObjectContext.tblMedlems.DeleteObject(tblMedlem);
            }
        }
    }
}


