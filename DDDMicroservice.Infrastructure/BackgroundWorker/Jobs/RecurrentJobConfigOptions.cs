using DDDMicroservice.Infrastructure.Jobs;

namespace DDDMicroservice.Infrastructure.Configuration
{
    public abstract class RecurrentJobConfigOptions : IRecurrentJobConfigOptions
    {
        /// <summary>
        /// check for new accounts every x minutes
        /// </summary>
        public int frequency { get; set; } = 1;
        public string JobId { get; set; } = Guid.NewGuid().ToString();
    }
}