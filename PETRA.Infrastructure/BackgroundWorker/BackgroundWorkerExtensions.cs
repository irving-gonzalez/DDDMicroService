using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PETRA.Infrastructure.BackgroundWorker
{
    public static class BackgroundWorkerExtensions
    {
        public static void AddBackgroundWorker(this IServiceCollection services, string connection)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(connection));

            //hangfire server
            //services.AddHangfireServer();
        }

        public static void UseBackgroundWorkerDashboard(this WebApplication app)
        {
            app.UseHangfireDashboard();
        }
    }
}