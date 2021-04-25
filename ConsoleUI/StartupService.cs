using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;


namespace ConsoleUI
{
    public class StartupService : IStartupService
    {
        private readonly ILogger<StartupService> logger;
        private readonly IConfiguration config;

        public StartupService(ILogger<StartupService> logger, IConfiguration config)
        {
            this.logger = logger;
            this.config = config;
        }

        public void Run()
        {
            for (int i = 0; i < config.GetValue<int>("LoopTimes"); i++)
            {
                logger.LogInformation("Run numbber {runNumber}", i);
            }
        }
    }
}
