using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public partial class MainViewModel
    {
        private Task LoadSyncItemsImpl()
        {
            Items.Clear();
            var ids = stringService.GetIndicies();
            ItemCount = ids.Count;
            foreach (var x in ids)
            {
                var str = stringService.GetString(x);
                Items.Insert(0, str);
            }

            return Task.CompletedTask;
        }
    }
}
