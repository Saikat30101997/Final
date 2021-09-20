using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Membership.BusinessObjects
{
    public class AccessRequirementHandler :
          AuthorizationHandler<AccessRequirement> 
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               AccessRequirement requirement)
        {
            var claim = context.User.FindFirst("access_permission");
            if (claim != null && bool.Parse(claim.Value))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
