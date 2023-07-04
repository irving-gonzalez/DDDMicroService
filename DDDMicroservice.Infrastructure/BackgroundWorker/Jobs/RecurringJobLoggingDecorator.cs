using Hangfire.Server;
using Microsoft.Extensions.Logging;

namespace DDDMicroservice.Infrastructure.Jobs
{
    public class RecurringJobLoggingDecorator : IRecurrentJob
    {
        public IRecurrentJobConfigOptions Options { get; set; }
        private readonly IRecurrentJob _recurrentJob;
        private readonly ILogger<IRecurrentJob> _logger;

        public RecurringJobLoggingDecorator(IRecurrentJob recurrentJob, ILogger<IRecurrentJob> logger)
        {
            _recurrentJob = recurrentJob;
            _logger = logger;
            Options = recurrentJob.Options;
        }

        public Task Execute(PerformContext context)
        {
            var logProperties = new Dictionary<string, object>
            {
                ["JobId"] = context.BackgroundJob.Id,
            };

            using (_logger.BeginScope(logProperties))
            {
                return _recurrentJob.Execute(context);
            }
        }

        public bool JobExists()
        {
            return _recurrentJob.JobExists();
        }
    }
}