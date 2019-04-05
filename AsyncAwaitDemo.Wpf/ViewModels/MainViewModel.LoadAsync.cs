using AsyncAwaitDemo.Wpf.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

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
                Debug.WriteLine($"Idx: {x} - Thread: {Thread.CurrentThread.ManagedThreadId}");
                Items.Add(str);
            }
        }

        public ICommand LoadAsyncItemsCommand { get; private set; }

        private void InitLoadAsync()
        {
            LoadAsyncItemsCommand = new MainViewModelCommand(LoadAsyncItemImpl, this);
        }
    }
}
