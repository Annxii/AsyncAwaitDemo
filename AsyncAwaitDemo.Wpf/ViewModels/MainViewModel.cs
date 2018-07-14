using AsyncAwaitDemo.Wpf.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace AsyncAwaitDemo.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly StringService stringService = new StringService();

        private bool isExecuting = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            Items = new ObservableCollection<string>();
            LoadSyncItemsCommand = new MainViewModelCommand(() => { LoadSyncItemsImpl(); return Task.CompletedTask; }, this);
            LoadAsyncItemsCommand = new MainViewModelCommand(LoadAsyncItemImpl, this);
            LoadAsyncItemsParallelCommand = new MainViewModelCommand(LoadAsyncItemsParallelImpl, this);
            VoidSafeExceptionCommand = new MainViewModelCommand(VoidExceptionImpl, this);
            VoidExceptionCommand = new RelayCommand(async () => await VoidExceptionImpl());
        }

        public bool IsExecuting
        {
            get { return isExecuting; }
            set
            {
                if(isExecuting != value)
                {
                    isExecuting = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExecuting)));
                }
            }
        }

        public ObservableCollection<string> Items { get; }

        public ICommand LoadSyncItemsCommand { get; }

        public ICommand LoadAsyncItemsCommand { get; }

        public ICommand LoadAsyncItemsParallelCommand { get; }

        public ICommand VoidSafeExceptionCommand { get; }

        public ICommand VoidExceptionCommand { get; }

        private void LoadSyncItemsImpl()
        {
            Items.Clear();
            var ids = stringService.GetTop100().Result;
            foreach (var x in ids)
            {
                var str = stringService.GetString(x).Result;
                Items.Insert(0, str);
            }
        }

        private async Task LoadAsyncItemImpl()
        {
            Items.Clear();
            var ids = await stringService.GetTop100();
            foreach (var x in ids)
            {
                var str = await stringService.GetString(x);
                Items.Insert(0, str);
            }
        }

        private async Task LoadAsyncItemsParallelImpl()
        {
            Items.Clear();
            var dispatcher = Dispatcher.CurrentDispatcher;
            var ids = await stringService.GetTop100();
            var tasks = ids.Select(x => Task.Run(async () =>
            {
                var str = await stringService.GetString(x);
                await dispatcher.InvokeAsync(() => Items.Insert(0, str));
            }));

            await Task.WhenAll(tasks);
        }

        private async Task VoidExceptionImpl()
        {
            await Task.Delay(500).ConfigureAwait(false);
            throw new ApplicationException("Async/Await void exception!");
        }
    }
}
