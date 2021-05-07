using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;
using R5T.Langobard;


namespace R5T.Plymouth.Construction
{
    public class Startup : Plymouth.Startup.Startup
    {
        public override async Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IServiceProvider startupServiceProvider)
        {
            await base.ConfigureConfiguration(configurationBuilder, startupServiceProvider);

            configurationBuilder.AddJsonFile("appsettings.json");
        }

        public override async Task ConfigureServices(IServiceCollection services, IServiceAction<IConfiguration> configurationAction, IServiceProvider startupServiceProvider)
        {
            await base.ConfigureServices(services, configurationAction, startupServiceProvider);

            services.AddLogging(LoggingBuilderHelper.AddDefaultLogging);

            services.AddSingleton<DummyService>();
        }
    }
}
