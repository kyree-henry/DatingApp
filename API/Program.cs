using DatingApp.API.Data.Core;

namespace DatingApp.API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            using IServiceScope? scope = host.Services.CreateScope();
            IServiceProvider? sp = scope.ServiceProvider;
            try
            {
                DataContext? context = sp.GetRequiredService<DataContext>();
                await context?.Database?.MigrateAsync()!;

                await Seed.SeedUsers(context);
            }
            catch (Exception ex)
            {
                ILogger? logger = sp.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
               Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStaticWebAssets();
                   webBuilder.UseStartup<Startup>();
               });
    }
}