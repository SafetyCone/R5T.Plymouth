using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using R5T.Dacia;

using IMicrosoftHostBuilder = Microsoft.Extensions.Hosting.IHostBuilder;


namespace R5T.Plymouth.Host
{
    public class HostBuilder : IApplicationBuilder<IHost>
    {
        // Not and Action<> since we will always only be using one host builder.
        private IMicrosoftHostBuilder MicrosoftHostBuilder { get; }


        public HostBuilder(IMicrosoftHostBuilder microsoftHostBuilder)
        {
            this.MicrosoftHostBuilder = microsoftHostBuilder;
        }

        public HostBuilder()
            : this(HostBuilderHelper.GetDefaultHostBuilder())
        {
        }

        public Task<IHost> Build(IApplicationSpecification applicationSpecification)
        {
            var hostBuilder = this.MicrosoftHostBuilder;

            // Configuration.
            hostBuilder.ConfigureAppConfiguration(configurationBuilder =>
            {
                Task.Run(async () =>
                {
                    foreach (var configureConfigurationAction in applicationSpecification.ConfigureConfigurationActions)
                    {
                        await configureConfigurationAction(configurationBuilder);
                    }
                }).Wait(); // Bad, sync-over-async, but the IHostBuilder interface is synchronous so no choice.
            });

            // Services.
            // The host builder puts the configuration into the services already, so null op.
            var configurationAction = ServiceAction<IConfiguration>.AlreadyAdded;

            hostBuilder.ConfigureServices(services =>
            {
                Task.Run(async () =>
                {
                    foreach (var configureServicesAction in applicationSpecification.ConfigureServicesActions)
                    {
                        await configureServicesAction(services, configurationAction);
                    }
                }).Wait(); // Bad, sync-over-async, but the IHostBuilder interface is synchronous so no choice.
            });

            // Done.
            var host = hostBuilder.Build();

            return Task.FromResult(host);
        }
    }
}
