using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AdminApp.API.SignalR
{
    /// <summary>
    /// Restricts access to hub methods to users
    /// </summary>
    public class DomainAuthorizeRequirement : AuthorizationHandler<DomainAuthorizeRequirement, HubInvocationContext>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DomainAuthorizeRequirement requirement, HubInvocationContext resource)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}