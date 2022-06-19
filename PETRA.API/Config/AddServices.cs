using System.Reflection;
using MediatR;
using PETRA.API.Config;
using PETRA.Infrastructure.DataAccess.Extesions;
using PETRA.Infrastructure.ServiceBus.Extesions;

namespace PETRA.Application.Config
{
    public static class ConfigureServices
    {
        public static void AddServices(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var configuration = configurationManager.Get<AppConfiguration>();
            services.AddDataAccess(configurationManager.GetConnectionString("UserManager"));
            services.AddServiceBus(configuration.ServiceBus);           
            services.AddMediatR(Assembly.GetExecutingAssembly());       
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}