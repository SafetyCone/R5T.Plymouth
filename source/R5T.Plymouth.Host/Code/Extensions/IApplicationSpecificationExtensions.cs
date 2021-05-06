using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using IMicrosoftHostBuilder = Microsoft.Extensions.Hosting.IHostBuilder;


namespace R5T.Plymouth.Host
{
    public static class IApplicationSpecificationExtensions
    {
        public static Task<IHost> BuildHost(this IApplicationSpecification applicationSpecification, HostBuilder hostBuilder)
        {
            return hostBuilder.Build(applicationSpecification);
        }

        public static Task<IHost> BuildHost(this IApplicationSpecification applicationSpecification, IMicrosoftHostBuilder microsoftHostBuilder)
        {
            var hostBuilder = new HostBuilder(microsoftHostBuilder);

            return applicationSpecification.BuildHost(hostBuilder);
        }

        public static Task<IHost> BuildHost(this IApplicationSpecification applicationSpecification)
        {
            var microsoftHostBuilder = HostBuilderHelper.GetDefaultHostBuilder();

            return applicationSpecification.BuildHost(microsoftHostBuilder);
        }
    }
}
