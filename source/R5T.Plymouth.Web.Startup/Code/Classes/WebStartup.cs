using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;


namespace R5T.Plymouth.Web.Startup
{
    public class WebStartup : Plymouth.Startup.Startup, IWebStartup
    {
        /// <summary>
        /// When overridden, startup base methods should always be called as the first line of the override method.
        /// However, this base method does nothing.
        /// </summary>
        public Task Configure(IApplicationBuilder applicationBuilder, IServiceProvider startupServiceProvider)
        {
            // Do nothing.

            return Task.CompletedTask;
        }
    }
}
