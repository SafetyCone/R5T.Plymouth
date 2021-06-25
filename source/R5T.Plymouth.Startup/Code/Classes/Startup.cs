using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;


namespace R5T.Plymouth.Startup
{
    public class Startup : IStartup
    {
        /// <summary>
        /// When overridden, startup base methods should always be called as the first line of the override method.
        /// However, this base method does nothing.
        /// </summary>
        public virtual Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IServiceProvider startupServicesProvider)
        {
            // Do nothing.

            return Task.CompletedTask;
        }

        /// <summary>
        /// When overridden, startup base methods should always be called as the first line of the override method.
        /// However, this base method does nothing.
        /// </summary>
        public virtual Task ConfigureServices(IServiceCollection services, IServiceAction<IConfiguration> configurationAction, IServiceProvider startupServicesProvider)
        {
            // Do nothing.

            return Task.CompletedTask;
        }
    }
}
