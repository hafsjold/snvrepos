using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.LightSwitch;
using Microsoft.LightSwitch.Security.Server;
namespace LightSwitchApplication
{
    public partial class ApplicationDataService
    {
        partial void tblProjekts_CanDelete(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.CanDeleteProekt);
        }

        partial void tblProjekts_CanUpdate(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.CanUpdateProjekt);
        }

        partial void tblProjekts_CanInsert(ref bool result)
        {
            result = this.Application.User.HasPermission(Permissions.CanInsertProjekt);
        }
    }
}
