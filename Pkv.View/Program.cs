
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using System.IO;

namespace Pkv.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostContext, builder) =>
                 {
                     if (hostContext.HostingEnvironment.IsDevelopment())
                     {
                         builder.AddUserSecrets<Program>();
                     }

                     builder.AddEnvironmentVariables();
                 })
                .ConfigureLogging(logging =>
                {
                    //logging.AddAzureWebAppDiagnostics();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseStartup<Startup>();
                });
    }
}
