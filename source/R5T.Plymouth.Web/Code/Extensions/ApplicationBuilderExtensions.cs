using System;
using System.Threading.Tasks;

using R5T.Plymouth.Web;


namespace R5T.Plymouth
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplicationSpecification NewWebApplicationSynchronous(this ApplicationBuilder applicationBuilder)
        {
            var webApplicationSpecification = new WebApplicationSpecification();
            return webApplicationSpecification;
        }

        public static Task<IWebApplicationSpecification> NewWebApplication(this ApplicationBuilder applicationBuilder)
        {
            var webApplicationSpecfication = applicationBuilder.NewWebApplicationSynchronous();

            return Task.FromResult(webApplicationSpecfication as IWebApplicationSpecification);
        }
    }
}
