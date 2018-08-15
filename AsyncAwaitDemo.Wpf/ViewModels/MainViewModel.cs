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
    public partial class MainViewModel : INotifyPropertyChanged
    {
        private readonly StringService stringService = new StringService();

        public MainViewModel()
        {
            Items = new ObservableCollection<string>();
            LoadSyncItemsCommand = new MainViewModelCommand(LoadSyncItemsImpl, this);
            LoadAsyncItemsCommand = new MainViewModelCommand(LoadAsyncItemImpl, this);
            LoadAsyncItemsParallelCommand = new MainViewModelCommand(LoadAsyncItemsParallelImpl, this);
            VoidSafeExceptionCommand = new MainViewModelCommand(VoidExceptionImpl, this, ex =>
            {
                Items.Clear();
                ItemCount = 0;
                Items.Add(ex.Message);
            });
            VoidExceptionCommand = new RelayCommand(async () => await VoidExceptionImpl());
        }

        #region UI setup
        private bool isExecuting = false;
        public bool IsExecuting
        {
            get { return isExecuting; }
            set
            {
                if (isExecuting != value)
                {
                    isExecuting = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExecuting)));
                }
            }
        }

        private int itemCount = 0;
        public int ItemCount
        {
            get { return itemCount; }
            set
            {
                if (itemCount != value)
                {
                    itemCount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ItemCount)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Items { get; }

        public ICommand LoadSyncItemsCommand { get; }

        public ICommand LoadAsyncItemsCommand { get; }

        public ICommand LoadAsyncItemsParallelCommand { get; }

        public ICommand VoidSafeExceptionCommand { get; }

        public ICommand VoidExceptionCommand { get; }
        #endregion

    }
}
