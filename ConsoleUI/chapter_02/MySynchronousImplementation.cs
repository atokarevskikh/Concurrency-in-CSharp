using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI.chapter_02
{
    /// <summary>
    /// Возвращение завершенных задач.
    /// Синхронные методы с асинхронной сигнатурой.
    /// Могут использоваться в тестах как простая заглушка для асинхронного интерфейса.
    /// При реализации асинхронного интерфейса синхронным кодом, следует избегать любых форм блокировки!
    /// </summary>
    public class MySynchronousImplementation : IMyAsyncInterface
    {
        public Task DoSomethingAsync()
        {
            // Если может произойти исключение
            try
            {
                DoSomethingSynchronously();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task<int> GetValueAsync()
        {
            return Task.FromResult(42);
        }

        public Task<int> GetValueAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled<int>(cancellationToken);
            return Task.FromResult(13);
        }

        public Task<T> NotImplementedAsync<T>()
        {
            return Task.FromException<T>(new NotImplementedException());
        }

        private void DoSomethingSynchronously()
        {
            // Do something...
        }
    }
}
