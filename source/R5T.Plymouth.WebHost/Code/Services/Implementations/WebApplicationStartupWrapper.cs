using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;

using R5T.Dacia;

using R5T.Plymouth.Web;

using IAspNetCoreStartup = Microsoft.AspNetCore.Hosting.IStartup;


namespace R5T.Plymouth.WebHost
{
    /// <summary>
    /// Wraps an <see cref="IWebStartup"/> instance as an <see cref="Microsoft.AspNetCore.Hosting.IStartup"/> instance.
    /// </summary>
    public class WebApplicationStartupWrapper : IAspNetCoreStartup
    {
        private IServiceAction<IConfiguration> ConfigurationAction { get; }
        private IWebApplicationSpecification WebApplicationSpecification { get; }


        public WebApplicationStartupWrapper(
            IWebApplicationSpecification webApplicationSpecification,
            IServiceAction<IConfiguration> configurationAction)
        {
            this.ConfigurationAction = configurationAction;
            this.WebApplicationSpecification = webApplicationSpecification;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Task.Run(async () =>
            {
                foreach (var configureServicesAction in this.WebApplicationSpecification.ConfigureServicesActions)
                {
                    await configureServicesAction(services, this.ConfigurationAction);
                }
            }).Wait(); // Bad, sync-over-async, but the Microsoft.AspNetCore.Hosting.IStartup interface (and IWebHostBuilder interface) is synchronous so no choice.

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app)
        {
            Task.Run(async () =>
            {
                foreach (var configureAction in this.WebApplicationSpecification.ConfigureActions)
                {
                    await configureAction(app);
                }
            }).Wait(); // Bad, sync-over-async, but the Microsoft.AspNetCore.Hosting.IStartup interface (and IWebHostBuilder interface) is synchronous so no choice.
        }
    }
}
