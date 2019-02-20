using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace postgresqlDemo.Web.Host.Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            InitBuildWebHost(args).Run();
        }

        public static IWebHost InitBuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
