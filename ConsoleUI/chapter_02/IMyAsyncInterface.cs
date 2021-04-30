using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI.chapter_02
{
    interface IMyAsyncInterface
    {
        Task<int> GetValueAsync();
        Task<int> GetValueAsync(CancellationToken cancellationToken);
        Task DoSomethingAsync();
        Task<T> NotImplementedAsync<T>();
    }
}
