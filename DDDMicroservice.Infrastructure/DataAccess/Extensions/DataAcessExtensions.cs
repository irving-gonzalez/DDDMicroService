using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DDDMicroservice.Domain.AggregatesModel;
using DDDMicroservice.Infrastructure.DataAccess.Repositories;

namespace DDDMicroservice.Infrastructure.DataAccess.Extesions
{
    public static class DataAccessExtensions
    {
        public static void AddDataAccess(this IServiceCollection services, string connectionString)
        {
            //dependencies
            //Todo
            // if (!services.Any(x => x.ServiceType == typeof(IMapper)))
            // {
            //     services.AddAutoMapper(typeof(DataAccessExtensions));
            // }
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddDbContext<DatabaseContext>(o => //o.UseLazyLoadingProxies()
                                                        o.UseNpgsql(connectionString));
        }
    }
}