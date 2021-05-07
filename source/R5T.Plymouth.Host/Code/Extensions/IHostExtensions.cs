using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;


namespace R5T.Plymouth
{
    public static class IHostExtensions
    {
        public static async Task Run(this Task<IHost> gettingHost)
        {
            var host = await gettingHost;

            await host.RunAsync();
        }
    }
}
