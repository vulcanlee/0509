using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using XFListView.Models;
using XFListView.Services;

namespace XFListView.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<MyItem> MyTasks { get; set; } =
            new ObservableCollection<MyItem>();
        public MyItem MyTaskSelected { get; set; }
        private readonly INavigationService _navigationService;
        public DelegateCommand TapCommand { get; set; }
        public  bool IsRefreshing { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            TapCommand = new DelegateCommand(() =>
            {
                NavigationParameters para = new NavigationParameters();
                para.Add("current", MyTaskSelected);
                _navigationService.NavigateAsync("DetailPage", para);
            });
            RefreshCommand = new DelegateCommand(() =>
            {
                Refresh();
                IsRefreshing = false;
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Refresh();
        }

        void Refresh()
        {
            var foo = MyTaskService.GetAllTask();
            foreach (var item in foo)
            {
                MyTasks.Add(item);
            }
        }
    }
}
