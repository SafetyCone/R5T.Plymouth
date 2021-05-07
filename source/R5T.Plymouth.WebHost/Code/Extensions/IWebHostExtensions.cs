using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;


namespace R5T.Plymouth
{
    public static class IWebHostExtensions
    {
        public static async Task Run(this Task<IWebHost> gettingWebHost)
        {
            var webHost = await gettingWebHost;

            await webHost.RunAsync();
        }
    }
}
