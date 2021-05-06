using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;


namespace R5T.Plymouth.Web
{
    public class WebApplicationSpecification : ApplicationSpecification, IWebApplicationSpecification
    {
        public List<Func<IApplicationBuilder, Task>> ConfigureActions { get; } = new List<Func<IApplicationBuilder, Task>>();
    }
}
