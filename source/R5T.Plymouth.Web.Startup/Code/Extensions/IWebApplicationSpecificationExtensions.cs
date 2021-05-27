using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

using R5T.Dacia;

using R5T.Plymouth.Startup;
using R5T.Plymouth.Web;
using R5T.Plymouth.Web.Startup;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth
{
    public static class IWebApplicationSpecificationExtensions
    {
        #region Synchronous

        public static TWebApplicationSpecification UseWebStartupSynchronous<TWebApplicationSpecification>(this TWebApplicationSpecification webApplicationSpecification, IWebStartup webStartup, IServiceProvider startupServiceProvider)
            where TWebApplicationSpecification: IWebApplicationSpecification
        {
            // Take care of ConfigureConfiguration() and ConfigureServices().
            webApplicationSpecification.UseStartupSynchronous(webStartup, startupServiceProvider);

            // Now Configure().
            Task Configure(IApplicationBuilder applicationBuilder)
            {
                return webStartup.Configure(applicationBuilder, startupServiceProvider);
            }
            webApplicationSpecification.ConfigureActions.Add(Configure);

            return webApplicationSpecification;
        }

        public static TWebApplicationSpecification UseWebStartupSynchronous<TWebApplicationSpecification, TWebStartup>(this TWebApplicationSpecification webApplicationSpecification, IServiceProvider startupServiceProvider)
            where TWebApplicationSpecification : IWebApplicationSpecification
            where TWebStartup : class, IWebStartup
        {
            var webStartup = ServiceProviderHelper.GetInstanceOfType<TWebStartup>();

            webApplicationSpecification.UseWebStartupSynchronous(webStartup, startupServiceProvider);

            return webApplicationSpecification;
        }

        public static TWebApplicationSpecification UseWebStartupSynchronous<TWebApplicationSpecification, TWebStartup>(this TWebApplicationSpecification webApplicationSpecification)
            where TWebApplicationSpecification : IWebApplicationSpecification
            where TWebStartup : class, IWebStartup
        {
            using (var emptyServiceProvider = ServiceProviderHelper.GetNewEmptyServiceProvider())
            {
                return webApplicationSpecification.UseWebStartupSynchronous<TWebApplicationSpecification, TWebStartup>(emptyServiceProvider);
            }
        }

        #endregion

        #region Asynchronous-by-default

        public static async Task<TWebApplicationSpecification> UseWebStartup<TWebApplicationSpecification>(this Task<TWebApplicationSpecification> gettingWebApplicationSpecification, IWebStartup webStartup, IServiceProvider startupServiceProvider)
            where TWebApplicationSpecification : IWebApplicationSpecification
        {
            var webApplicationSpecification = await gettingWebApplicationSpecification;

            return webApplicationSpecification.UseWebStartupSynchronous(webStartup, startupServiceProvider);
        }

        public static async Task<TWebApplicationSpecification> UseWebStartup<TWebApplicationSpecification, TWebStartup>(this Task<TWebApplicationSpecification> gettingWebApplicationSpecification, IServiceProvider startupServiceProvider)
            where TWebApplicationSpecification : IWebApplicationSpecification
            where TWebStartup : class, IWebStartup
        {
            var webApplicationSpecification = await gettingWebApplicationSpecification;

            return webApplicationSpecification.UseWebStartupSynchronous<TWebApplicationSpecification, TWebStartup>(startupServiceProvider);
        }

        public static async Task<TWebApplicationSpecification> UseWebStartup<TWebApplicationSpecification, TWebStartup>(this Task<TWebApplicationSpecification> gettingWebApplicationSpecification)
            where TWebApplicationSpecification : IWebApplicationSpecification
            where TWebStartup : class, IWebStartup
        {
            var webApplicationSpecification = await gettingWebApplicationSpecification;

            return webApplicationSpecification.UseWebStartupSynchronous<TWebApplicationSpecification, TWebStartup>();
        }

        #endregion

        #region IWebApplicationSpecification Specific

        public static Task<IWebApplicationSpecification> UseWebStartup<TWebStartup>(this Task<IWebApplicationSpecification> gettingWebApplicationSpecification, IServiceProvider startupServiceProvider)
            where TWebStartup : class, IWebStartup
        {
            return gettingWebApplicationSpecification.UseWebStartup<IWebApplicationSpecification, TWebStartup>(startupServiceProvider);
        }

        public static Task<IWebApplicationSpecification> UseWebStartup<TWebStartup>(this Task<IWebApplicationSpecification> gettingWebApplicationSpecification)
            where TWebStartup : class, IWebStartup
        {
            return gettingWebApplicationSpecification.UseWebStartup<IWebApplicationSpecification, TWebStartup>();
        }

        #endregion

        #region Multi-Stage Startup

        public static async Task<TWebApplicationSpecification> UseWebStartup<TWebApplicationSpecification, TWebStartup>(this Task<TWebApplicationSpecification> gettingApplicationSpecification, Func<Task<MicrosoftServiceProvider>> startupServiceProviderConstructor)
            where TWebApplicationSpecification : IWebApplicationSpecification
            where TWebStartup : class, IWebStartup
        {
            using (var startupServiceProvider = await startupServiceProviderConstructor())
            {
                return await gettingApplicationSpecification.UseStartup<TWebApplicationSpecification, TWebStartup>(startupServiceProvider);
            }
        }

        public static Task<IWebApplicationSpecification> UseWebStartup<TWebStartup>(this Task<IWebApplicationSpecification> gettingApplicationSpecification, Func<Task<MicrosoftServiceProvider>> startupServiceProviderConstructor)
            where TWebStartup : class, IWebStartup
        {
            return gettingApplicationSpecification.UseWebStartup<IWebApplicationSpecification, TWebStartup>(startupServiceProviderConstructor);
        }

        public static Task<IWebApplicationSpecification> UseTwoStageWebStartup<TWebStartup, TConfigurationStartup>(this Task<IWebApplicationSpecification> gettingApplicationSpecification)
            where TWebStartup : class, IWebStartup
            where TConfigurationStartup : class, IStartup
        {
            return gettingApplicationSpecification.UseWebStartup<TWebStartup>(() =>
            {
                return ApplicationBuilder.Instance
                    .NewApplication()
                    .UseStartup<TConfigurationStartup>()
                    .BuildServiceProvider();
            });

        }

        #endregion
    }
}
