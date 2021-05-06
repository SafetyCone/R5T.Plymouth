using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

using R5T.Dacia;
using R5T.Herulia.Extensions;

using R5T.Plymouth.Web;

using AspNetCoreStartup = Microsoft.AspNetCore.Hosting.IStartup;
using AspNetCoreWebHostBuilder = Microsoft.AspNetCore.Hosting.WebHostBuilder;


namespace R5T.Plymouth.WebHost
{
    public class WebHostBuilder : IWebApplicationBuilder<IWebHost>
    {
        public async Task<IWebHost> Build(IWebApplicationSpecification webApplicationSpecification)
        {
            // Configuration.
            var configurationBuilder = new ConfigurationBuilder();

            foreach (var configureConfigurationAction in webApplicationSpecification.ConfigureConfigurationActions)
            {
                await configureConfigurationAction(configurationBuilder);
            }

            var configuration = configurationBuilder.Build();

            // The webhost builder puts the configuration into the services with the UseConfiguration() extrension, so null op.
            var configurationAction = ServiceAction<IConfiguration>.AddedElsewhere;

            var startupWrapper = new WebApplicationStartupWrapper(webApplicationSpecification, configurationAction);

            var webHostBuilder = new AspNetCoreWebHostBuilder()
                .UseConfiguration(configuration)
                .UseKestrel()
                .UseDefaultContentRoot()
                .UseIISIntegration()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<AspNetCoreStartup>(startupWrapper);
                })
                ;

            var webHost = webHostBuilder.Build();
            return webHost;
        }
    }
}
