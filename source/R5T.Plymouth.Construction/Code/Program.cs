using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;


namespace R5T.Plymouth.Construction
{
    class Program
    {
        private void ExampleCode()
        {
            // Does not work if you dispose the configuration startup provider.
            //return ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<Startup, ConfigurationStartup>()
            //    .UseProgramAsAService<Program>()
            //    .BuildProgramAsAServiceHost()
            //    .Run();

            //return ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<Startup>(() =>
            //    {
            //        return ApplicationBuilder.Instance
            //            .NewApplication()
            //            .UseStartup<ConfigurationStartup>()
            //            .BuildServiceProvider();
            //    })
            //    .UseProgramAsAService<Program>()
            //    .BuildProgramAsAServiceHost()
            //    .Run();

            //return ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<Startup>(
            //        ServiceProviderBuilder.UseStartup<ConfigurationStartup>())
            //    .UseProgramAsAService<Program>()
            //    .BuildProgramAsAServiceHost()
            //    .Run();

            //// Three-stage startup.
            //return ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<Startup>(
            //        ServiceProviderBuilder.UseStartup<ConfigurationStartup>(
            //            ServiceProviderBuilder.UseStartup<ConfigurationStartup>()))
            //    .UseProgramAsAService<Program>()
            //    .BuildProgramAsAServiceHost()
            //    .Run();

            // No good, somehow the inner await does not actually apply?
            //IHost host;
            //using (var startupServiceProvider = await ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<ConfigurationStartup>()
            //    .BuildServiceProvider())
            //{
            //    host = await ApplicationBuilder.Instance
            //        .NewApplication()
            //        .UseStartup<Startup>()
            //        .UseProgramAsAService<Program>()
            //        .BuildProgramAsAServiceHost();
            //}

            //await host.RunAsync();

            //// Same. No good, somehow the inner await does not actually apply?
            //using (var startupServiceProvider = await ApplicationBuilder.Instance
            //    .NewApplication()
            //    .UseStartup<ConfigurationStartup>()
            //    .BuildServiceProvider())
            //{
            //    // Somehow no good.
            //    await ApplicationBuilder.Instance
            //        .NewApplication()
            //        .UseStartup<Startup>()
            //        .UseProgramAsAService<Program>()
            //        .BuildProgramAsAServiceHost()
            //        .Run();
            //}
        }


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
