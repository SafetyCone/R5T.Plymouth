using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;


namespace R5T.Plymouth
{
    /// <summary>
    /// Representation for an object that configures a configuration and service collection.
    /// </summary>
    /// <remarks>
    /// Asynchronous. Provides access to a <see cref="IServiceProvider"/> from which services can be requested for use in configuration of the configuration and service collection.
    /// </remarks>
    public interface IStartup
    {
        Task ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IServiceProvider startupServiceProvider);
        Task ConfigureServices(IServiceCollection services, IServiceAction<IConfiguration> configurationAction, IServiceProvider startupServiceProvider);
    }
}
