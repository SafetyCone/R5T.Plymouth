using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;

using R5T.Plymouth.Startup;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth
{
    public static class IApplicationSpecificationExtensions
    {
        #region Synchronous

        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification>(this TApplicationSpecification applicationSpecification, IStartup startup, IServiceProvider startupServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
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

        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification, TStartup>(this TApplicationSpecification applicationSpecification, IServiceProvider startupServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var startup = ServiceProviderHelper.GetInstanceOfType<TStartup>();

            applicationSpecification.UseStartupSynchronous(startup, startupServiceProvider);

            return applicationSpecification;
        }

        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification, TStartup>(this TApplicationSpecification applicationSpecification)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            using (var emptyServiceProvider = ServiceProviderHelper.GetNewEmptyServiceProvider())
            {
                return applicationSpecification.UseStartupSynchronous<TApplicationSpecification, TStartup>(emptyServiceProvider);
            }
        }

        #endregion

        #region Asynchronous-by-Default

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification>(this Task<TApplicationSpecification> gettingApplicationSpecification, IStartup startup, IServiceProvider startupServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous(startup, startupServiceProvider);
        }

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification, IServiceProvider startupServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous<TApplicationSpecification, TStartup>(startupServiceProvider);
        }

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous<TApplicationSpecification, TStartup>();
        }

        #endregion

        #region IApplicationSpecification Specific

        public static Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification, IServiceProvider startupServiceProvider)
            where TStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseStartup<IApplicationSpecification, TStartup>(startupServiceProvider);
        }

        public static async Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification, Task<MicrosoftServiceProvider> gettingStartupServiceProvider)
            where TStartup : class, IStartup
        {
            var startupServiceProvider = await gettingStartupServiceProvider;

            return await gettingApplicationSpecification.UseStartup<TStartup>(startupServiceProvider);
        }

        public static Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification)
            where TStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseStartup<IApplicationSpecification, TStartup>();
        }

        #endregion
    }
}
