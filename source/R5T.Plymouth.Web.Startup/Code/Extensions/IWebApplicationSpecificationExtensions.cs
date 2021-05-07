using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;

using R5T.Dacia;

using R5T.Plymouth.Web;
using R5T.Plymouth.Web.Startup;


namespace R5T.Plymouth
{
    public static class IWebApplicationSpecificationExtensions
    {
        public static IWebApplicationSpecification UseWebStartup(this IWebApplicationSpecification webApplicationSpecification, IWebStartup webStartup, IServiceProvider startupServiceProvider)
        {
            // Take care of ConfigureConfiguration() and ConfigureServices().
            webApplicationSpecification.UseStartup(webStartup, startupServiceProvider);

            // Now Configure().
            Task Configure(IApplicationBuilder applicationBuilder)
            {
                return webStartup.Configure(applicationBuilder, startupServiceProvider);
            }
            webApplicationSpecification.ConfigureActions.Add(Configure);

            return webApplicationSpecification;
        }

        public static IWebApplicationSpecification UseWebStartup<TWebStartup>(this IWebApplicationSpecification webApplicationSpecification, IServiceProvider startupServiceProvider)
            where TWebStartup : class, IWebStartup
        {
            var webStartup = ServiceProviderHelper.GetInstanceOfType<TWebStartup>();

            webApplicationSpecification.UseWebStartup(webStartup, startupServiceProvider);

            return webApplicationSpecification;
        }

        public static IWebApplicationSpecification UseWebStartup<TWebStartup>(this IWebApplicationSpecification webApplicationSpecification)
            where TWebStartup : class, IWebStartup
        {
            using (var emptyServiceProvider = ServiceProviderHelper.GetNewEmptyServiceProvider())
            {
                return webApplicationSpecification.UseWebStartup<TWebStartup>(emptyServiceProvider);
            }
        }
    }
}
