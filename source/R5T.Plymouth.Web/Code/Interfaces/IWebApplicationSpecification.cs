using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using IAspNetCoreApplicationBuilder = Microsoft.AspNetCore.Builder.IApplicationBuilder;


namespace R5T.Plymouth.Web
{
    public interface IWebApplicationSpecification : IApplicationSpecification
    {
        List<Func<IAspNetCoreApplicationBuilder, Task>> ConfigureActions { get; }
    }
}
