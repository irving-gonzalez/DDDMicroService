using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;

namespace DDDMicroservice.Application.Authorization;

public class HamgFireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        var identity = context.GetHttpContext().User.Identity;
        if (identity != null && identity.IsAuthenticated)
        {
            return true;
        }

        context.GetHttpContext().ChallengeAsync().Wait();
        return false;
    }
}
