using PETRA.Infrastructure.BackgroundWorker;

namespace PETRA.Application.Config
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
                app.UseBackgroundWorkerDashboard();
            }

        }
    }
}