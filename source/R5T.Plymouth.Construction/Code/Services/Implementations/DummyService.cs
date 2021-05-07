using System;

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

        public void SayHello()
        {
            Console.WriteLine("Hello!");
        }

        public void LogInformationHello()
        {
            this.Logger.LogInformation("Hello!");
        }
    }
}
