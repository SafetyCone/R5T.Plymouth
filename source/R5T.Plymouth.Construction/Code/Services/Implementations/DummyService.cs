using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;


namespace R5T.Plymouth.Construction
{
    public class DummyService
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
