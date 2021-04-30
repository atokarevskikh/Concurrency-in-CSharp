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

            var task =TestMySynchronousImplementation();
            task.Wait();

            logger.LogInformation("Method Run stoped");
        }

        async Task TestDelay()
        {
            logger.LogInformation("Start testing delay result");
            var delay = new chapter_02.Delay();
            int delayInSeconds = 3;
            var result = await delay.DelayResult(42, TimeSpan.FromSeconds(delayInSeconds));
            logger.LogInformation("After delay {delayInSeconds} seconds returned {result}.", delayInSeconds, result);
            logger.LogInformation("End testing delay result");
        }

        async Task TestRetry()
        {
            logger.LogInformation("Start testing download with retries");
            string uri = "http://localhost:81";
            var delay = new chapter_02.Delay();
            using (var httpClient = new HttpClient())
            {
                var htmlPage = await delay.DownloadStringWithRetries(httpClient, uri);
                logger.LogInformation("Length of page is {pageLength}", htmlPage.Length);
            }
            logger.LogInformation("End testing download with retries");
        }

        async Task TestMySynchronousImplementation()
        {
            var mySynchronousImplementation = new chapter_02.MySynchronousImplementation();
            var result = await mySynchronousImplementation.GetValueAsync();
            logger.LogInformation("result = {result}", result);
            try
            {
                await mySynchronousImplementation.NotImplementedAsync<int>();
                logger.LogInformation("This will not be print.");
            }
            catch (NotImplementedException ex)
            {
                logger.LogInformation("NotImplementedException was thrown. {message}", ex.Message);
            }
        }
    }
}
