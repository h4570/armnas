using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WebApi
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, builder) =>
                 builder.SetBasePath(context.HostingEnvironment.ContentRootPath)).
            UseStartup<Startup>();
    }
}
