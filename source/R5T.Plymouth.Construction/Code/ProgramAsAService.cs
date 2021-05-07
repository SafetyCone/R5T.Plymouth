using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using R5T.Plymouth.ProgramAsAService;


namespace R5T.Plymouth.Construction
{
    public class ProgramAsAService : ProgramAsAServiceBase
    {
        #region Static

        public static Task SubMain()
        {
            return ApplicationBuilder.Instance
                .NewApplication()
                .UseStartup<Startup>()
                .UseProgramAsAService<ProgramAsAService>()
                .BuildProgramAsAServiceHost()
                .Run();
        }

        #endregion


        private DummyService DummyService { get; }


        public ProgramAsAService(
            IApplicationLifetime applicationLifetime,
            DummyService dummyService)
            : base(applicationLifetime)
        {
            this.DummyService = dummyService;
        }

        protected override async Task ServiceMain(CancellationToken stoppingToken)
        {
            await this.DummyService.SayHello();
            await this.DummyService.LogInformationHello();
        }
    }
}
