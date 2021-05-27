using System;
using System.Threading.Tasks;


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
    }
}
