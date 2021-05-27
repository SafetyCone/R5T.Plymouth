using System;
using System.Threading.Tasks;

using R5T.Plymouth.ServiceProvider;
using R5T.Plymouth.Startup;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth
{
    public static class IApplicationSpecificationExtensions
    {
        public static Task<MicrosoftServiceProvider> BuildServiceProviderSynchronous(this IApplicationSpecification applicationSpecification)
        {
            return ServiceProviderBuilder.New().Build(applicationSpecification);
        }

        public static async Task<MicrosoftServiceProvider> BuildServiceProvider(this Task<IApplicationSpecification> gettingApplicationSpecification)
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return await ServiceProviderBuilder.New().Build(applicationSpecification);
        }

        #region Multi-Stage Startup

        public static async Task<TApplicationSpecification> UseStartup<TApplicationSpecification, TStartup>(this Task<TApplicationSpecification> gettingApplicationSpecification, Func<Task<MicrosoftServiceProvider>> startupServiceProviderConstructor)
            where TApplicationSpecification : IApplicationSpecification
            where TStartup : class, IStartup
        {
            // Ok to not have no using context. This service provider needs to be available during the application anyway else it would be disposed by time it is used in the 
            var startupServiceProvider = await startupServiceProviderConstructor();

            var applicationSpecification = await gettingApplicationSpecification.UseStartup<TApplicationSpecification, TStartup>(startupServiceProvider);
            return applicationSpecification;
        }

        public static Task<IApplicationSpecification> UseStartup<TStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification, Func<Task<MicrosoftServiceProvider>> startupServiceProviderConstructor)
            where TStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseStartup<IApplicationSpecification, TStartup>(startupServiceProviderConstructor);
        }

        public static Task<IApplicationSpecification> UseTwoStageStartup<TStartup, TConfigurationStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification)
            where TStartup : class, IStartup
            where TConfigurationStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseStartup<TStartup>(() =>
            {
                return ApplicationBuilder.Instance
                    .NewApplication()
                    .UseStartup<TConfigurationStartup>()
                    .BuildServiceProvider();
            });
        }

        public static Task<IApplicationSpecification> UseStartup<TStartup, TConfigurationStartup>(this Task<IApplicationSpecification> gettingApplicationSpecification)
            where TStartup : class, IStartup
            where TConfigurationStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseTwoStageStartup<TStartup, TConfigurationStartup>();
        }

        #endregion
    }
}
