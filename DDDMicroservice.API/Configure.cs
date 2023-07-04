using Hangfire;
using DDDMicroservice.Application.Authorization;
using DDDMicroservice.Application.MiddleWare;
using DDDMicroservice.Infrastructure.BackgroundWorker;

namespace DDDMicroservice.Application.Config
{
    public static class AppConfigurationExtensions
    {
        public static void Configure(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1");
                    c.RoutePrefix = string.Empty;
                });
            }
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseBackgroundWorkerDashboard(new DashboardOptions
            {
                Authorization = new[] { new HamgFireDashboardAuthorizationFilter() },
                StatsPollingInterval = 60000
            });
        }
    }
}