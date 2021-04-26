using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
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
            logger.LogInformation("Method Run started");
            Task taskDelay = TestDelay();
            logger.LogInformation("Method Run continued");

            try
            {
                Task taskRetry = TestRetry();
                taskRetry.Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

            taskDelay.Wait();
            logger.LogInformation("Method Run stoped");
        }

        async Task TestDelay()
        {
            logger.LogInformation("Start testing delay result");
            var delay = new chapter_02.Delay(logger);
            int delayInSeconds = 3;
            var result = await delay.DelayResult(42, TimeSpan.FromSeconds(delayInSeconds));
            logger.LogInformation("After delay {delayInSeconds} seconds returned {result}.", delayInSeconds, result);
            logger.LogInformation("End testing delay result");
        }

        async Task TestRetry()
        {
            logger.LogInformation("Start testing download with retries");
            string uri = "http://localhost:81";
            var delay = new chapter_02.Delay(logger);
            using (var httpClient = new HttpClient())
            {
                var htmlPage = await delay.DownloadStringWithRetries(httpClient, uri);
                logger.LogInformation("Length of page is {pageLength}", htmlPage.Length);
            }
            logger.LogInformation("End testing download with retries");
        }
    }
}
