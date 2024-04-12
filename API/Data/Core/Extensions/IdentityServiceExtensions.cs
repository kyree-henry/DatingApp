using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApp.API.Data.Core.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(cfg =>
                    {
                        cfg.TokenValidationParameters = new()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });


            return services;
        }
    }
}