using AsyncAwaitDemo.Wpf.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncAwaitDemo.Wpf.Util
{
    public class MainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> asyncAction;
        private readonly Action<Exception> exceptionCallback;
        private readonly MainViewModel mainViewModel;

        public MainViewModelCommand(Func<Task> asyncAction, MainViewModel mainViewModel, Action<Exception> exceptionCallback = null)
        {
            this.asyncAction = asyncAction;
            this.mainViewModel = mainViewModel;
            this.exceptionCallback = exceptionCallback ?? (ex => Debug.WriteLine(ex));
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            mainViewModel.IsExecuting = true;
            try
            {
                await asyncAction.Invoke();
            }
            catch (Exception ex)
            {
                exceptionCallback.Invoke(ex);
            }
            finally
            {
                mainViewModel.IsExecuting = false;
            }
        }
    }
}
