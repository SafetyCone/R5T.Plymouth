using System;
using System.Threading.Tasks;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth.ServiceProvider
{
    public static class IApplicationSpecificationExtensions
    {
        public static Task<MicrosoftServiceProvider> BuildServiceProvider(this IApplicationSpecification applicationSpecification)
        {
            return ServiceProviderBuilder.New().Build(applicationSpecification);
        }
    }
}
