using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.LightSwitch.Server;
using Microsoft.LightSwitch.Security;
using Microsoft.LightSwitch.Security.Server;
using LightSwitchApplication;

namespace LightSwitchApplication.Perms
{
    public class UserPermissionsController : ApiController
    {
        // GET api/<controller>
        public Dictionary<string, Boolean> Get()
        {
            Dictionary<string, Boolean> perms = new Dictionary<string, Boolean>();

            using (ServerApplicationContext ctx = ServerApplicationContext.CreateContext())
            {
                var currentUser = ctx.Application.User;
                if (currentUser.IsAuthenticated)
                {
                    perms.Add(Permissions.SecurityAdministration,
                        currentUser.HasPermission(Permissions.SecurityAdministration));

                    currentUser.AddPermissions(Permissions.SecurityAdministration);

                    foreach (Permission perm in ctx.DataWorkspace.SecurityData.Permissions)
                    {
                        if (perm.Id != Permissions.SecurityAdministration)
                        {
                            perms.Add(perm.Id, currentUser.HasPermission(perm.Id));
                        }
                    }
                }
            }
            return perms;
        }
    }
}