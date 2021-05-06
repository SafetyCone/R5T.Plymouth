using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;


namespace R5T.Plymouth.ProgramAsAService
{
    // Hosted service program implemented using background service for simplicity and ease of use provided by the background service base class.
    public abstract class ProgramAsAServiceBase : BackgroundService
    {
        private IApplicationLifetime ApplicationLifetime { get; }


        public ProgramAsAServiceBase(
            IApplicationLifetime applicationLifetime)
        {
            this.ApplicationLifetime = applicationLifetime;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return this.ServiceMainWrapper(stoppingToken);
        }

        private async Task ServiceMainWrapper(CancellationToken stoppingToken)
        {
            try
            {
                await this.ServiceMain(stoppingToken);
            }
            finally
            {
                // Stop the application when it is done.
                this.ApplicationLifetime.StopApplication();
            }
        }

        // Named "ServiceMain" to avoid collision with .NET required Program.Main() entry point.
        protected abstract Task ServiceMain(CancellationToken stoppingToken);
    }
}
