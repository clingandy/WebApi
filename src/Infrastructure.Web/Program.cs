using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:8000")
                .UseStartup<Startup>()
                .Build();
    }
}
