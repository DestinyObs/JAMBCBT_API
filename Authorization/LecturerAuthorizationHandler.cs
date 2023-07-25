using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using JAMBAPI.Models;

namespace JAMBAPI.Authorization
{
    public class LecturerAuthorizationHandler : AuthorizationHandler<LecturerRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LecturerRequirement requirement)
        {
            // Check if the user is authenticated
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            // Check if the user is a lecturer
            if (context.User.HasClaim(c => c.Type == "IsLecturer" && c.Value == "true"))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
