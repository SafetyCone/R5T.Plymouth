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

        /// <summary>
        /// This is the actual implementation method, taking an <see cref="IStartup"/> instance and a <see cref="IServiceProvider"/> of services for use in the execution of the startup instance's methods.
        /// </summary>
        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification>(this TApplicationSpecification applicationSpecification,
            IStartup startup,
            IServiceProvider startupServicesProvider)
            where TApplicationSpecification : IApplicationSpecification
        {
            Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder)
            {
                return startup.ConfigureConfiguration(configurationBuilder, startupServicesProvider);
            }
            applicationSpecification.ConfigureConfigurationActions.Add(ConfigureConfiguration);

            Task ConfigureServices(IServiceCollection services, IServiceAction<IConfiguration> configurationAction)
            {
                return startup.ConfigureServices(services, configurationAction, startupServicesProvider);
            }
            applicationSpecification.ConfigureServicesActions.Add(ConfigureServices);

            return applicationSpecification;
        }

        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification, TStartup>(this TApplicationSpecification applicationSpecification,
            IServiceProvider startupServicesProvider)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var startup = ServiceProviderHelper.GetInstanceOfType<TStartup>();

            applicationSpecification.UseStartupSynchronous(startup, startupServicesProvider);

            return applicationSpecification;
        }

        /// <summary>
        /// Allows providing the service provider for use in providing the <typeparamref name="TStartup"/>: <see cref="IStartup"/> instance (<paramref name="startupProvidingServiceProvider"/>).
        /// <para>Note: the <typeparamref name="TStartup"/> type must be available from the service provider as a <typeparamref name="TStartup"/>.</para>
        /// </summary>
        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification, TStartup>(this TApplicationSpecification applicationSpecification,
            IServiceProvider startupServicesProvider,
            IServiceProvider startupProvidingServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var startup = startupProvidingServiceProvider.GetRequiredService<TStartup>();

            applicationSpecification.UseStartupSynchronous(startup, startupServicesProvider);

            return applicationSpecification;
        }

        public static TApplicationSpecification UseStartupSynchronous<TApplicationSpecification, TStartup>(this TApplicationSpecification applicationSpecification,
            IServiceProvider startupServicesProvider,
            Action<ServiceCollection> configureStartupProviderServices)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var startup = ServiceCollectionHelper.GetInstanceOfTypeSynchronous<TStartup>(configureStartupProviderServices);

            return applicationSpecification.UseStartupSynchronous(
                startup,
                startupServicesProvider);
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

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification>(this Task<TApplicationSpecification> gettingApplicationSpecification,
            IStartup startup,
            IServiceProvider startupServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous(startup, startupServiceProvider);
        }

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification,
            IServiceProvider startupServiceProvider)
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

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification,
            IServiceProvider startupServicesProvider,
            IServiceProvider startupProvidingServiceProvider)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous<TApplicationSpecification, TStartup>(
                startupServicesProvider,
                startupProvidingServiceProvider);
        }

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification,
            IServiceProvider startupServicesProvider,
            Action<ServiceCollection> configureStartupProviderServices)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseStartupSynchronous<TApplicationSpecification, TStartup>(
                startupServicesProvider,
                configureStartupProviderServices);
        }

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification,
            IServiceProvider startupServicesProvider,
            Func<ServiceCollection, Task> configureStartupProviderServices)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            var startup = await ServiceCollectionHelper.GetInstanceOfType<TStartup>(configureStartupProviderServices);

            return applicationSpecification.UseStartupSynchronous(
                startup,
                startupServicesProvider);
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

        public static async Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification,
            IServiceProvider startupServicesProvider,
            Func<ServiceCollection, Task> configureStartupProviderServices)
            where TStartup : class, IStartup
        {
            var applicationSpecification = await gettingApplicationSpecification;

            var startup = await ServiceCollectionHelper.GetInstanceOfType<TStartup>(configureStartupProviderServices);

            return applicationSpecification.UseStartupSynchronous(
                startup,
                startupServicesProvider);
        }

        public static Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification,
            Func<ServiceCollection, Task> configureStartupProviderServices)
            where TStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseStartup<TStartup>(
                ServiceProviderHelper.GetNewEmptyServiceProvider(),
                configureStartupProviderServices);
        }

        #endregion
    }
}
