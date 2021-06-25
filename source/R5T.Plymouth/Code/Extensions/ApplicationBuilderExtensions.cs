using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;


namespace R5T.Plymouth
{
    public static class ApplicationBuilderExtensions
    {
        public static ApplicationSpecification NewApplicationSynchronous(this ApplicationBuilder applicationBuilder)
        {
            var applicationSpecification = new ApplicationSpecification();
            return applicationSpecification;
        }

        public static Task<IApplicationSpecification> NewApplication(this ApplicationBuilder applicationBuilder)
        {
            var applicationSpecfication = applicationBuilder.NewApplicationSynchronous();

            return Task.FromResult(applicationSpecfication as IApplicationSpecification);
        }

        public static async Task<IApplicationSpecification> AddConfigureServicesAction(this Task<IApplicationSpecification> gettingApplicationSpecification,
            Func<IServiceCollection, IServiceAction<IConfiguration>, Task> configureServicesAction)
        {
            var applicationSpecification = await gettingApplicationSpecification;

            applicationSpecification.ConfigureServicesActions.Add(configureServicesAction);

            return applicationSpecification;
        }
    }
}
