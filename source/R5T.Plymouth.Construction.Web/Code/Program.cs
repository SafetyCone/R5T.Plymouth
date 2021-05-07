using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using R5T.Plymouth;


namespace R5T.Plymouth.Construction.Web
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            //return Program.RunDefaultMain(args);
            return Program.RunWebHost();
        }

        private static Task RunWebHost()
        {
            return ApplicationBuilder.Instance
                .NewWebApplication()
                .UseWebStartup<Startup>()
                .BuildWebHost()
                .Run();
        }

        /// <summary>
        /// This is the main as created by the ASP.NET Core project template.
        /// </summary>
        private static Task RunDefaultMain(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            return Task.CompletedTask;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                .UseStartup<DefaultStartup>();
    }
}
