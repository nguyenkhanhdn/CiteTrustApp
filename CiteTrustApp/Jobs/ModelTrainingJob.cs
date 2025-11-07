using System;
using CiteTrustApp.Models;
using CiteTrustApp.Services;

namespace CiteTrustApp.Jobs
{
    public static class ModelTrainingJob
    {
        // Hangfire requires a public parameterless method for recurring jobs by default.
        public static void Run()
        {
            try
            {
                using (var db = new CTSDbContext())
                using (var svc = new MLRecommendationService(db))
                {
                    // tune iterations/rank as needed
                    svc.TrainMatrixFactorization(iterations: 20, rank: 32);
                }
            }
            catch (Exception ex)
            {
                // keep logs for diagnosis
                System.Diagnostics.Trace.TraceError($"ModelTrainingJob failed: {ex}");
                throw;
            }
        }
    }
}