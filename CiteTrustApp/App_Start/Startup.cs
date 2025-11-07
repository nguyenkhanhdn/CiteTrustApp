//csharp CiteTrustApp\Startup.cs
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using CiteTrustApp.Jobs;

[assembly: OwinStartup(typeof(CiteTrustApp.Startup))]
namespace CiteTrustApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // Hangfire configuration - use a connection string name defined in Web.config
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("AcademicEvidenceDb"); // replace with your connection string name

            // Dashboard (secure it - see note)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() } // restrict access
            });

            // Start Hangfire Server
            app.UseHangfireServer();

            // Schedule recurring job: daily at 2:00 AM (example)
            RecurringJob.AddOrUpdate("train-ml-model", () => ModelTrainingJob.Run(), Cron.Daily(2));

            // You can also call RecurringJob.AddOrUpdate(...) from an admin action to change frequency.
        }
    }
}