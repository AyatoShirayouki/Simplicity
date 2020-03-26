using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Simplicity.Requirement
{
    public class RoleHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var user = context.User;
            if (context.User.IsInRole("User"))
            {
                context.Succeed(requirement);
            }
            var claim = context.User.FindFirst(ClaimTypes.Role);
            var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var redirectContext = context.Resource as AuthorizationFilterContext;
            return Task.CompletedTask;
        }
    }
}
