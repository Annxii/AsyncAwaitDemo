using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public partial class MainViewModel
    {
        private async Task VoidExceptionImpl()
        {
            await Task.Delay(500).ConfigureAwait(false);
            throw new ApplicationException("Async/Await void exception!");
        }

    }
}
