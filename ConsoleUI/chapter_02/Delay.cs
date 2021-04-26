using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.chapter_02
{
    public class Delay
    {
        private readonly ILogger logger;

        public Delay(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Требуется (асинхронно) приостановить выполнение программы на некоторый период времени. 
        /// Такая ситуация часто встречается при модульном тестировании или реализации задержки для повторного использования. 
        /// Она также возникает при программировании простых тайм-аутов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }

        /// <summary>
        /// Экспоненциальная задержка — стратегия увеличения задержек между повторными попытками.
        /// В реальном коде лучше применить более качественное решение (например, использующее библиотеку Polly NuGet).
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<string> DownloadStringWithRetries(HttpClient client, string uri)
        {
            // Повторить попытку через 1 секунду, потом через 2 и через 4 секунды.
            TimeSpan nextDelay = TimeSpan.FromSeconds(1);
            for (int i = 0; i != 3; ++i)
            {
                try
                {
                    logger.LogInformation("Request. Next delay will be {nextDelay}", nextDelay);
                    return await client.GetStringAsync(uri);
                }
                catch
                {
                }
                await Task.Delay(nextDelay);
                nextDelay = nextDelay + nextDelay;
            }
            // Попробовать в последний раз и разрешить распространение ошибки.
            logger.LogInformation("Last request.");
            return await client.GetStringAsync(uri);
        }
    }
}
