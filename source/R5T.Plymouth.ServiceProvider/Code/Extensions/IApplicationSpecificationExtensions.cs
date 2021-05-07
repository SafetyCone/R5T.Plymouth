using System;
using System.Threading.Tasks;

using R5T.Plymouth.ServiceProvider;

using MicrosoftServiceProvider = Microsoft.Extensions.DependencyInjection.ServiceProvider;


namespace R5T.Plymouth
{
    public static class IApplicationSpecificationExtensions
    {
        public static Task<MicrosoftServiceProvider> BuildServiceProvider(this IApplicationSpecification applicationSpecification)
        {
            return ServiceProviderBuilder.New().Build(applicationSpecification);
        }
    }
}
