using AsyncAwaitDemo.Wpf.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AsyncAwaitDemo.Wpf.Util
{
    public class MainViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Func<Task> @delegate;
        private readonly MainViewModel mainViewModel;

        public MainViewModelCommand(Func<Task> @delegate, MainViewModel mainViewModel)
        {
            this.@delegate = @delegate;
            this.mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter) => true;

        public async void Execute(object parameter)
        {
            mainViewModel.IsExecuting = true;
            try
            {
                await @delegate.Invoke();
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                mainViewModel.IsExecuting = false;
            }
        }
    }
}
