using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0064;


namespace R5T.Plymouth.Construction
{
    [ServiceImplementationMarker]
    public class DummyService: INoServiceDefinition, IServiceImplementation
    {
        private ILogger Logger { get; }


        public DummyService(
            ILogger<DummyService> logger)
        {
            this.Logger = logger;
        }

        public Task SayHello()
        {
            Console.WriteLine("Hello!");

            return Task.CompletedTask;
        }

        public Task LogInformationHello()
        {
            this.Logger.LogInformation("Hello!");

            return Task.CompletedTask;
        }
    }
}
