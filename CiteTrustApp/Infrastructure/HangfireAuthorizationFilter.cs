//csharp CiteTrustApp\Infrastructure\HangfireAuthorizationFilter.cs
using Hangfire.Dashboard;
using System.Web;

namespace CiteTrustApp.Infrastructure
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var http = HttpContext.Current;
            // only allow authenticated users in "Admin" role; customize to your needs
            return http?.User?.Identity?.IsAuthenticated == true && http.User.IsInRole("Admin");
        }
    }
}