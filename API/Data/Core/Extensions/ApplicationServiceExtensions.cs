using DatingApp.API.Services.Interfaces;
using DatingApp.API.Services;
using System.Reflection;
using DatingApp.API.Data.Helpers;

namespace DatingApp.API.Data.Core.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection(nameof(CloudinarySettings)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("SQLServerDefaultConnection"));
            });

            return services;
        }
    }
}