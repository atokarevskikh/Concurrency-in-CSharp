using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.chapter_01
{
    public class Delay
    {
        /// <summary>
        /// Требуется (асинхронно) приостановить выполнение программы на некоторый период времени. Такая ситуация часто встречается при модульном тестировании или реализации задержки для повторного использования. Она также возникает при программировании простых тайм-аутов.
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
    }
}
