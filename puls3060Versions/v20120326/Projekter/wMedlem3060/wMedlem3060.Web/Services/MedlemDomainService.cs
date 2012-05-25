
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
        [RequiresRole("Administrator")]
        public IQueryable<tblMedlem> GetTblMedlems()
        {
            return this.ObjectContext.tblMedlems.OrderBy(e=>e.Nr);
        }

        [RequiresRole("Administrator")]
        public IQueryable<tblMedlem> GetYoungTblMedlems(DateTime pFodtdato)
        {
            return this.ObjectContext.tblMedlems.Where(e => e.FodtDato < pFodtdato).OrderBy(e => e.Nr);
        }

        [RequiresRole("Administrator")]
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

        [RequiresRole("Administrator")]
        public void UpdateTblMedlem(tblMedlem currenttblMedlem)
        {
            this.ObjectContext.tblMedlems.AttachAsModified(currenttblMedlem, this.ChangeSet.GetOriginal(currenttblMedlem));
        }

        [RequiresRole("Administrator")]
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

        
        [RequiresRole("Administrator")]
        public IQueryable<vMedlemLog> GetvMedlemLogs()
        {
            return this.ObjectContext.vMedlemLogs.OrderBy(e => e.Nr);
        }


        [RequiresRole("Administrator")]
        public IQueryable<tblMedlemLog> GetTblMedlemLogs()
        {
            return this.ObjectContext.tblMedlemLogs.OrderBy(e => e.Nr);
        }


        [RequiresRole("Administrator")]
        public void InsertTblMedlemLog(tblMedlemLog tblMedlemLog)
        {
            if ((tblMedlemLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tblMedlemLog, EntityState.Added);
            }
            else
            {
                this.ObjectContext.tblMedlemLogs.AddObject(tblMedlemLog);
            }
        }

        [RequiresRole("Administrator")]
        public void UpdateTblMedlemLog(tblMedlemLog currenttblMedlemLog)
        {
            this.ObjectContext.tblMedlemLogs.AttachAsModified(currenttblMedlemLog, this.ChangeSet.GetOriginal(currenttblMedlemLog));
        }

        [RequiresRole("Administrator")]
        public void DeleteTblMedlemLog(tblMedlemLog tblMedlemLog)
        {
            if ((tblMedlemLog.EntityState != EntityState.Detached))
            {
                this.ObjectContext.ObjectStateManager.ChangeObjectState(tblMedlemLog, EntityState.Deleted);
            }
            else
            {
                this.ObjectContext.tblMedlemLogs.Attach(tblMedlemLog);
                this.ObjectContext.tblMedlemLogs.DeleteObject(tblMedlemLog);
            }
        }

        [RequiresRole("Administrator")]
        public IQueryable<tblAktivitet> GetTblAktivitets()
        {
            return this.ObjectContext.tblAktivitets.OrderBy(e => e.id);
        }
    }
}


