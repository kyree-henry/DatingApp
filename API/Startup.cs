using DatingApp.API.Data.Core.Extensions;
using DatingApp.API.Data.Core.Middleware;

namespace DatingApp.API
{
    public class Startup(IConfiguration config)
    {
        private readonly IConfiguration _config = config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication(_config);
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 5001;
            });
            services.AddIdentity(_config);

            services.AddCors();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(policy => {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
