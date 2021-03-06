using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Contas.API
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => 
                {
                    #if !DEBUG
                        webBuilder.UseStartup<Startup>().UseUrls("http://*:" + System.Environment.GetEnvironmentVariable("PORT"));
                    #else
                        webBuilder.UseStartup<Startup>();
                    #endif
                });
    }
}
