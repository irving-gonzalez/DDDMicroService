using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PETRA.Infrastructure.ServiceBus.Extesions
{
    public class ServiceBusOptions
    {
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = "5672";
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public static class ServiceBusExtensions
    {
        public static void AddServiceBus(this IServiceCollection services, ServiceBusOptions configOptions)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumer<MessageConsumer>(typeof(MessageConsumerDefinition));
                 x.AddConsumers(typeof(MessageConsumer).Assembly);
       
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configOptions.Host, ushort.Parse(configOptions.Port) ,"/", h =>
                    {
                        h.Username(configOptions.Username);
                        h.Password(configOptions.Password);
                    });         
                       
                    cfg.ConfigureEndpoints(context);
                });              
            });
        }
    }
}