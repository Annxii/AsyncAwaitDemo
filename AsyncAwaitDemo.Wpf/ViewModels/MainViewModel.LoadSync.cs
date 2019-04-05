using AsyncAwaitDemo.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                Items.Add(str);
            }

            return Task.CompletedTask;
        }

        public ICommand LoadSyncItemsCommand { get; private set; }

        private void InitLoadSync()
        {
            LoadSyncItemsCommand = new MainViewModelCommand(LoadSyncItemsImpl, this);
        }

    }
}
