using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using R5T.Dacia;


namespace R5T.Plymouth
{
    public class ApplicationSpecification : IApplicationSpecification
    {
        public List<Func<IConfigurationBuilder, Task>> ConfigureConfigurationActions { get; } = new List<Func<IConfigurationBuilder, Task>>();
        public List<Func<IServiceCollection, IServiceAction<IConfiguration>, Task>> ConfigureServicesActions { get; } = new List<Func<IServiceCollection, IServiceAction<IConfiguration>, Task>>();
    }
}
