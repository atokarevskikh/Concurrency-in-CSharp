using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI.chapter_02
{
    public class Delay
    {
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
                    return await client.GetStringAsync(uri);
                }
                catch
                {
                }
                await Task.Delay(nextDelay);
                nextDelay = nextDelay + nextDelay;
            }
            // Попробовать в последний раз и разрешить распространение ошибки.
            return await client.GetStringAsync(uri);
        }

        /// <summary>
        /// Task.Delay также можно использовать для организации простого тайм-аута.
        /// Если в операции происходит тайм-аут, она не отменяется. 
        /// Здесь задача загрузки продолжит прием данных и загрузит весь ответ перед тем, как потерять его.
        /// Если нужно реализовать тайм-аут, лучшим кандидатом будет CancellationToken.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        async Task<string> DownloadStringWithTimeout(HttpClient client, string uri)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            Task<string> downloadTask = client.GetStringAsync(uri);
            Task timeoutTask = Task.Delay(Timeout.InfiniteTimeSpan, cts.Token);
            Task completedTask = await Task.WhenAny(downloadTask, timeoutTask);
            if (completedTask == timeoutTask)
                return null;
            return await downloadTask;
        }
    }
}
