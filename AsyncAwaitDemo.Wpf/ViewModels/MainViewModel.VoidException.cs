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
        private async Task VoidExceptionImpl()
        {
            await Task.Delay(500).ConfigureAwait(false);
            throw new ApplicationException("Async/Await void exception!");
        }

        private void VoidExceptionCallback(Exception ex)
        {
            Items.Clear();
            ItemCount = 0;
            Items.Add(ex.Message);
        }

        public ICommand VoidSafeExceptionCommand { get; private set; }

        public ICommand VoidExceptionCommand { get; private set; }

        private void InitVoidException()
        {
            VoidSafeExceptionCommand = new MainViewModelCommand(VoidExceptionImpl, this, VoidExceptionCallback);
            VoidExceptionCommand = new RelayCommand(async () => await VoidExceptionImpl());
        }

    }
}
