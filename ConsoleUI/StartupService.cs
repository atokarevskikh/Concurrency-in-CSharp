using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            TestDelay();
            Console.Read();
        }

        async void TestDelay()
        {
            logger.LogInformation("Start testing delay result");
            var delay = new chapter_01.Delay();
            int delayInSeconds = 3;
            var result = await delay.DelayResult(42, TimeSpan.FromSeconds(delayInSeconds));
            logger.LogInformation("After delay {delayInSeconds} seconds returned {result}.", delayInSeconds, result);
            logger.LogInformation("End testing delay result");
        }
    }
}
