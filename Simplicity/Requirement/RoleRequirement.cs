using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simplicity.Requirement
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public string RoleName { get; set; }
        public RoleRequirement(string roleName)
        {
            RoleName = roleName;
        }
    }
}
