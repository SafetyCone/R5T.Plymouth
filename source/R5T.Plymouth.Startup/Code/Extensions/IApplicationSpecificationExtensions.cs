using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;

using R5T.Plymouth.Startup;


namespace R5T.Plymouth
{
    public static class IApplicationSpecificationExtensions
    {
        public static IApplicationSpecification UseStartup(this IApplicationSpecification applicationSpecification, IStartup startup, IServiceProvider startupServiceProvider)
        {
            Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
            {
                return startup.ConfigureConfiguration(configurationBuilder, startupServiceProvider);
            }
            applicationSpecification.ConfigureConfigurationActions.Add(ConfigureConfiguration);

            Task ConfigureServices(IServiceCollection services, IServiceAction<IConfiguration> configurationAction)
            {
                return startup.ConfigureServices(services, configurationAction, startupServiceProvider);
            }
            applicationSpecification.ConfigureServicesActions.Add(ConfigureServices);

            return applicationSpecification;
        }

        public static IApplicationSpecification UseStartup<TStartup>(this IApplicationSpecification applicationSpecification, IServiceProvider startupServiceProvider)
            where TStartup : class, IStartup
        {
            var startup = ServiceProviderHelper.GetInstanceOfType<TStartup>();

            applicationSpecification.UseStartup(startup, startupServiceProvider);

            return applicationSpecification;
        }

        public static IApplicationSpecification UseStartup<TStartup>(this IApplicationSpecification applicationSpecification)
            where TStartup : class, IStartup
        {
            using (var emptyServiceProvider = ServiceProviderHelper.GetNewEmptyServiceProvider())
            {
                return applicationSpecification.UseStartup<TStartup>(emptyServiceProvider);
            }
        }
    }
}
