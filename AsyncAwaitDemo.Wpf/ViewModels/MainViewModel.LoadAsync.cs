using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public partial class MainViewModel
    {
        private async Task LoadAsyncItemImpl()
        {
            Items.Clear();
            var ids = await stringService.GetIndiciesAsync();
            ItemCount = ids.Count;
            foreach (var x in ids)
            {
                var str = await stringService.GetStringAsync(x);
                Items.Insert(0, str);
            }
        }

    }
}
