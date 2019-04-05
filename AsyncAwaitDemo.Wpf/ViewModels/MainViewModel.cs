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
using System.Reflection;
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
            var initMethods = GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name.StartsWith("Init"));

            foreach (var method in initMethods)
            {
                method.Invoke(this, null);
            }
        }

        #region UI setup
        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

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
        #endregion
    }
}
