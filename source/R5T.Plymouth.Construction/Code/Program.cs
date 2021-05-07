using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Plymouth.Construction
{
    class Program
    {
        static Task Main(string[] args)
        {
            //return Program.RunServiceProvider();
            return Program.RunProgramAsAService();
        }

        private static Task RunProgramAsAService()
        {
            return ProgramAsAService.SubMain();
        }

        private static async Task RunServiceProvider()
        {
            using (var serviceProvider = await ApplicationBuilder.Instance
                .NewApplication()
                .UseStartup<Startup>()
                .BuildServiceProvider())
            {
                var dummyService = serviceProvider.GetRequiredService<DummyService>();

                await dummyService.SayHello();
                await dummyService.LogInformationHello();
            }
        }
    }
}
