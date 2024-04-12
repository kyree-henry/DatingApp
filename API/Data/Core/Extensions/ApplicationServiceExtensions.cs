using DatingApp.API.Services.Interfaces;
using DatingApp.API.Services;

namespace DatingApp.API.Data.Core.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("SQLServerDefaultConnection"));
            });

            return services;
        }


    }
}