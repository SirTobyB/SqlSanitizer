using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SqlSanitizer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://0.0.0.0:5000")
                        .UseStartup<Startup>();
                });
    }
}