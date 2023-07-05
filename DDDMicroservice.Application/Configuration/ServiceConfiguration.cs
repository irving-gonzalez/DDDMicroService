using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using DDDMicroservice.Infrastructure.BackgroundWorker;
using DDDMicroservice.Infrastructure.DataAccess.Extesions;
using DDDMicroservice.Infrastructure.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using DDDMicroservice.Infrastructure.DataAccess;
using DDDMicroservice.Application.Providers;
using DDDMicroservice.Infrastructure.Clients;
using DDDMicroservice.Application.Authorization;
using DDDMicroservice.Application.Authorization.Extesions;
using Serilog;
using DDDMicroservice.Infrastructure.Jobs;
using DDDMicroservice.Infrastructure.ServiceBus.Extesions;
using DDDMicroservice.Domain.AggregatesModel;
using DDDMicroservice.Infrastructure.DataAccess.Repositories;

namespace DDDMicroservice.Application.Configuration
{
    public static class ConfigureServices
    {
        public static void AddLogging(this ConfigureHostBuilder host)
        {
            //create the logger and setup your sinks, filters and properties
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.Console()
                        //.WriteTo.File("../logs/log.txt")
                        //.WriteTo.File(new CompactJsonFormatter(), "../logs/log.json")
                        .WriteTo.Debug()
                        .CreateBootstrapLogger();

            host.UseSerilog();
        }

        public static void AddApplicationServices(this ConfigureHostBuilder host, ConfigurationManager configurationManager)
        {
            host.UseServiceProviderFactory<ContainerBuilder>(new AutofacServiceProviderFactory())
            .ConfigureServices((ctx, services) =>
            {
                services.AddSingleton<DapperContext>();
                services.AddScoped<IIdentityProvider, KeycloakIdentityProvider>();
                services.AddSingleton<DapperContext>(r => new DapperContext(configurationManager));
                services.AddTransient<IRestClient, RestClient>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddHttpClient();

                services.AddDataAccess(configurationManager.GetConnectionString("UserManager"));
                services.AddMediatR(Assembly.GetExecutingAssembly());

                services.AddAutoMapper(config => config.AddMaps(Assembly.GetEntryAssembly()));

                var appConfiguration = configurationManager.Get<AppConfigurationOptions>();
                services.AddBackgroundWorker(configurationManager.GetConnectionString("Hangfire"), appConfiguration.hangFireOptions.Storename);
                services.AddSingleton<RecurrentChildJobOptions>(r => appConfiguration.rebaseAccountsConfigOptions);
                services.AddSingleton<KeycloakOptions>(r => appConfiguration.keycloakOptions);

                //Enable Msg queue
                services.AddServiceBus(appConfiguration.ServiceBus);

                services.AddAuthentication(BasicAuthDefaults.BasicAuthScheme)
                    .AddBasic(BasicAuthDefaults.BasicAuthScheme, options =>
                    {
                        options.Users = appConfiguration.BasicAuthUsers;
                    });
            })
            .ConfigureContainer<ContainerBuilder>((ctx, container) =>
            {
                container.AddOutOfBoundDecorator();

                container.RegisterDecorator<RecurringJobLoggingDecorator, IRecurrentJob>();
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                container.RegisterAssemblyTypes(assemblies).Where(t => t == typeof(RecurringJobBase))
                            .As<IRecurrentJob>()
                            .AsSelf()
                            .SingleInstance();
            });
        }
    }
}