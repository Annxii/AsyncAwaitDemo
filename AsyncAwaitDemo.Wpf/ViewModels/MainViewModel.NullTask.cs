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
        public ICommand NullTaskCommand { get; private set; }

        private void InitNullTask()
        {
            NullTaskCommand = new MainViewModelCommand(NullTaskImpl, this, ex => throw ex);
        }

        private Task NullTaskImpl()
        {
            Items.Clear();
            var str = stringService.GetString(0);
            Items.Add(str);

            return null;
        }
    }
}
