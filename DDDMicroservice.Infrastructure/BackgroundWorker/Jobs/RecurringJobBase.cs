using Hangfire;
using Hangfire.Server;
using Hangfire.Storage;

namespace DDDMicroservice.Infrastructure.Jobs
{
    public abstract class RecurringJobBase : IRecurrentJob
    {
        private readonly IRecurringJobManager _recurringJobManager;
        protected readonly IBackgroundJobClient _backgroundJobs;
        protected readonly JobStorage _jobStorage;

        public IRecurrentJobConfigOptions Options { get; set; }

        public RecurringJobBase
        (
            IRecurringJobManager recurringJobManager,
            IRecurrentJobConfigOptions options,
            JobStorage jobStorage,
            IBackgroundJobClient backgroundJobs
        )
        {
            _recurringJobManager = recurringJobManager;
            Options = options;
            _jobStorage = jobStorage;
            _backgroundJobs = backgroundJobs;
        }

        public abstract Task Execute(PerformContext context);

        public bool JobExists()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                var recurring = connection.GetRecurringJobs().FirstOrDefault(p => p.Id == Options.JobId);

                if (recurring == null)
                {
                    // recurring job not found
                    return false;
                }
                else if (!recurring.NextExecution.HasValue)
                {
                    return true;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}