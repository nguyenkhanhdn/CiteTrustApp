//csharp CiteTrustApp\Startup.cs
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using CiteTrustApp.Jobs;
using CiteTrustApp.Infrastructure;

[assembly: OwinStartup(typeof(CiteTrustApp.Startup))]
namespace CiteTrustApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // IMPORTANT: initialize Hangfire storage BEFORE using any Hangfire API (server, dashboard, recurring jobs).
            // Uses connection string name "AcademicEvidenceDb" from Web.config — change if your connection string name differs.
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "AcademicEvidenceDb",
                    new SqlServerStorageOptions
                    {
                        CommandBatchMaxTimeout = System.TimeSpan.FromMinutes(5),
                        SlidingInvisibilityTimeout = System.TimeSpan.FromMinutes(5),
                        QueuePollInterval = System.TimeSpan.FromSeconds(15),
                        UseRecommendedIsolationLevel = true,
                        DisableGlobalLocks = true,
                        PrepareSchemaIfNecessary = true
                    });

            // Dashboard (secure it - ensure HangfireAuthorizationFilter restricts access)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            // Start Hangfire Server (after storage configured)
            app.UseHangfireServer();

            // Schedule recurring job: daily at 2:00 AM (example)
            RecurringJob.AddOrUpdate("train-ml-model", () => ModelTrainingJob.Run(), Cron.Daily(15));
        }
    }
}