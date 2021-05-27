using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Chamavia;
using R5T.Dacia;

using R5T.Plymouth.Startup;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth.ServiceProvider
{
    public class ServiceProviderBuilder : IApplicationBuilder<MicrosoftServiceProvider>
    {
        #region Static

        public static ServiceProviderBuilder New()
        {
            var output = new ServiceProviderBuilder();
            return output;
        }

        public static Task<MicrosoftServiceProvider> UseStartup<TStartup>(IServiceProvider startupServiceProvider)
            where TStartup : class, IStartup
        {
            return ApplicationBuilder.Instance
                .NewApplication()
                .UseStartup<TStartup>(startupServiceProvider)
                .BuildServiceProvider();
        }

        public static Task<MicrosoftServiceProvider> UseStartup<TStartup>()
            where TStartup : class, IStartup
        {
            var emptyServiceProvider = ServiceProviderHelper.GetNewEmptyServiceProvider();

            return ServiceProviderBuilder.UseStartup<TStartup>(emptyServiceProvider);
        }

        public static async Task<MicrosoftServiceProvider> UseStartup<TStartup>(Task<MicrosoftServiceProvider> gettingStartupServiceProvider)
            where TStartup: class, IStartup
        {
            var startupServiceProvider = await gettingStartupServiceProvider;

            return await ServiceProviderBuilder.UseStartup<TStartup>(startupServiceProvider);
        }

        #endregion


        public async Task<MicrosoftServiceProvider> Build(IApplicationSpecification applicationSpecification)
        {
            // Configuration.
            var configurationBuilder = new ConfigurationBuilder();

            foreach (var configureConfigurationAction in applicationSpecification.ConfigureConfigurationActions)
            {
                await configureConfigurationAction(configurationBuilder);
            }

            var configuration = configurationBuilder.Build();

            // Services.
            var services = new ServiceCollection();

            var configurationAction = services.AddConfigurationAction(configuration);

            services
                .Run(configurationAction)
                ;

            foreach (var configureServicesAction in applicationSpecification.ConfigureServicesActions)
            {
                await configureServicesAction(services, configurationAction);
            }

            // Done.
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
