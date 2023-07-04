using DDDMicroservice.Infrastructure.Configuration;

namespace DDDMicroservice.Application.Configuration
{
    public class RecurrentChildJobOptions : RecurrentJobConfigOptions
    {
        public int customProperty { get; set; }
    }
}