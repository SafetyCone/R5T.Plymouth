using System;
using System.Threading.Tasks;

using IAspNetCoreApplicationBuilder = Microsoft.AspNetCore.Builder.IApplicationBuilder;


namespace R5T.Plymouth
{
    /// <summary>
    /// Representation for an object that configures a configuration, service collection, and middleware pipeline.
    /// </summary>
    /// <remarks>
    /// Asynchronous. Provides access to a <see cref="IServiceProvider"/> from which services can be requested for use in configuration of the configuration and service collection.
    /// </remarks>
    public interface IWebStartup : IStartup
    {
        Task Configure(IAspNetCoreApplicationBuilder applicationBuilder, IServiceProvider startupServiceProvider);
    }
}
