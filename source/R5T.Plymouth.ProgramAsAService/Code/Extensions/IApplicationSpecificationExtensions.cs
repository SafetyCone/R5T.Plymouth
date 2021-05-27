using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using R5T.Plymouth.ProgramAsAService;

using HostBuilder = R5T.Plymouth.Host.HostBuilder;
using IMicrosoftHostBuilder = Microsoft.Extensions.Hosting.IHostBuilder;


namespace R5T.Plymouth
{
    public static class IApplicationSpecificationExtensions
    {
        public static IApplicationSpecification UseProgramAsAServiceSynchronous<TProgramAsAService>(this IApplicationSpecification applicationSpecification)
            where TProgramAsAService : class, IProgramAsAService
        {
            applicationSpecification.ConfigureServicesActions.Add((services, _) =>
            {
                services.AddHostedService<TProgramAsAService>();

                return Task.CompletedTask;
            });

            return applicationSpecification;
        }

        public static async Task<IApplicationSpecification> UseProgramAsAService<TProgramAsAService>(this Task<IApplicationSpecification> gettingApplicationSpecification)
            where TProgramAsAService : class, IProgramAsAService
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return applicationSpecification.UseProgramAsAServiceSynchronous<TProgramAsAService>();
        }

        public static Task<IHost> BuildProgramAsAServiceHostSynchronous(this IApplicationSpecification applicationSpecification, HostBuilder hostBuilder)
        {
            return applicationSpecification.BuildHost(hostBuilder);
        }

        public static Task<IHost> BuildProgramAsAServiceHostSynchronous(this IApplicationSpecification applicationSpecification, IMicrosoftHostBuilder microsoftHostBuilder)
        {
            return applicationSpecification.BuildHost(microsoftHostBuilder);
        }

        public static Task<IHost> BuildProgramAsAServiceHostSynchronous(this IApplicationSpecification applicationSpecification)
        {
            return applicationSpecification.BuildHost();
        }

        public static async Task<IHost> BuildProgramAsAServiceHost(this Task<IApplicationSpecification> gettingApplicationSpecification)
        {
            var applicationSpecification = await gettingApplicationSpecification;

            return await applicationSpecification.BuildHost();
        }
    }
}
