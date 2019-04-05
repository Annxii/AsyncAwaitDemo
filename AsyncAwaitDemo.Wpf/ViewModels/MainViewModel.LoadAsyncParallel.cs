using AsyncAwaitDemo.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public partial class MainViewModel
    {
        private async Task LoadAsyncItemsParallelImpl()
        {
            Items.Clear();
            var ids = await stringService.GetIndiciesAsync();
            ItemCount = ids.Count;
            var tasks = ids.Select(async (x) =>
            {
                var str = await stringService.GetStringAsync(x);
                Debug.WriteLine($"Idx: {x} - Thread: {Thread.CurrentThread.ManagedThreadId}");
                Items.Add(str);
            });

            await Task.WhenAll(tasks);
        }

        public ICommand LoadAsyncItemsParallelCommand { get; private set; }

        private void InitLoadAsyncParallel()
        {
            LoadAsyncItemsParallelCommand = new MainViewModelCommand(LoadAsyncItemsParallelImpl, this);
        }
    }
}
