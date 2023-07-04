using Hangfire.Server;

namespace DDDMicroservice.Infrastructure.Jobs
{
    public interface IRecurrentJobConfigOptions
    {
        /// <summary>
        /// check for new accounts every x minutes
        /// </summary>
        public int frequency { get; set; }
        public string JobId { get; set; }
    }

    public interface IRecurrentJob
    {
        IRecurrentJobConfigOptions Options { get; set; }
        Task Execute(PerformContext context);
        bool JobExists();
    }
}