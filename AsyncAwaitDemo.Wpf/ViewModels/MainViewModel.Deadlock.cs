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
        private void DeadlockImpl()
        {
            Items.Clear();
            var str = CallWrapper().Result;
            Items.Add(str);

            async Task<string> CallWrapper() => await stringService.GetStringAsync(0);
        }

        public ICommand DeadlockCommand { get; private set; }

        private void InitDeadlock()
        {
            DeadlockCommand = new RelayCommand(DeadlockImpl);
        }
    }
}
