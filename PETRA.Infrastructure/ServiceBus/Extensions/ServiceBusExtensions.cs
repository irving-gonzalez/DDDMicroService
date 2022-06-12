using System.Linq;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PETRA.Domain.AggregatesModel;
using PETRA.Infrastructure.DataAccess.Repositories;

namespace PETRA.Infrastructure.ServiceBus.Extesions
{
    public static class ServiceBusExtensions
    {
        public static void AddServiceBus(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host("localhost", 6672,"/", h =>
                        {
                            h.Username("guest");
                            h.Password("guest");
                        });
                    });
            });
           
             // OPTIONAL, but can be used to configure the bus options
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });
        }
    }
}