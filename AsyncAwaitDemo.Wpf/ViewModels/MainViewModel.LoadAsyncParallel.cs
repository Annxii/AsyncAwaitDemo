using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public partial class MainViewModel
    {
        private async Task LoadAsyncItemsParallelImpl()
        {
            Items.Clear();
            var dispatcher = Dispatcher.CurrentDispatcher;
            var ids = await stringService.GetIndiciesAsync();
            ItemCount = ids.Count;
            var tasks = ids.Select(x => Task.Run(async () =>
            {
                var str = await stringService.GetStringAsync(x);
                await dispatcher.InvokeAsync(() => Items.Insert(0, str));
            }));

            await Task.WhenAll(tasks);
        }

    }
}
