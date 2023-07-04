using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DDDMicroservice.Infrastructure.BackgroundWorker
{
    public static class BackgroundWorkerExtensions
    {
        public static void AddBackgroundWorker(this IServiceCollection services, string connection, string storename)
        {
            services.AddHangfire(configuration => configuration
                .UseConsole()
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .SelectSqlStore(connection, storename));

            //hangfire server
            services.AddHangfireServer();
        }

        public static void UseBackgroundWorkerDashboard(this WebApplication app, DashboardOptions options)
        {
            app.UseHangfireDashboard("/hangfireDashboard", options);
        }

        public static IGlobalConfiguration<JobStorage> SelectSqlStore(this IGlobalConfiguration configuration, string connection, string storeName)
        {
            switch (storeName)
            {
                case "Sqlserver":
                    return configuration.UseSqlServerStorage(connection);
                case "Postgres":
                    return configuration.UsePostgreSqlStorage(connection);
                default:
                    throw new ArgumentException("{0} is not a valid Store name for hangFire server", storeName);
            }
        }
    }


}