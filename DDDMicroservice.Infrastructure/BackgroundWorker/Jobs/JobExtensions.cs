using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMicroservice.Infrastructure.Jobs
{
    public static class JobExtensions
    {
        public static void RunRecurrentJobs(this WebApplication app)
        {
            var recurringJobs = app.Services.GetServices<IRecurrentJob>();
            foreach (var job in recurringJobs)
            {
                Register(app.Services, job);
            }
        }

        private static void Register<T>(IServiceProvider serviceProvider, T job) where T : IRecurrentJob
        {
            if (!job.JobExists())
            {
                var _recurringJobManager = serviceProvider.GetService<IRecurringJobManager>();
                _recurringJobManager.AddOrUpdate<T>(job.Options.JobId, (j) => j.Execute(null), $"*/{job.Options.frequency} * * * *");
            }
        }
    }
}