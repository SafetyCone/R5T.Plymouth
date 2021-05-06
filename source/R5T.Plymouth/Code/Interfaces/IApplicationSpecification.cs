using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;


namespace R5T.Plymouth
{
    /// <summary>
    /// An application is nothing more than a configured collection of services. Thus an application specification is a list of actions to build a configuration and a service collection.
    /// </summary>
    public interface IApplicationSpecification
    {
        List<Func<IConfigurationBuilder, Task>> ConfigureConfigurationActions { get; }
        List<Func<IServiceCollection, IServiceAction<IConfiguration>, Task>> ConfigureServicesActions { get; }
    }
}
