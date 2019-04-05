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
        private async void InvalidContextImpl()
        {
            Items.Clear();
            var str = await stringService.GetStringAsync(0).ConfigureAwait(false);
            Items.Add(str);
        }

        public ICommand InvalidContextCommand { get; private set; }

        private void InitInvalidContext()
        {
            InvalidContextCommand = new RelayCommand(InvalidContextImpl);
        }

    }
}
