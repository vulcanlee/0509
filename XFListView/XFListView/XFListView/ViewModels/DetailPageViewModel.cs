using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using XFListView.Models;

namespace XFListView.ViewModels
{
    public class DetailPageViewModel : INotifyPropertyChanged, INavigationAware
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public MyItem MyItemSelected { get; set; }
        private readonly INavigationService _navigationService;
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        public DetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            SaveCommand = new DelegateCommand(() =>
            {

            });
            DeleteCommand = new DelegateCommand(() =>
            {

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
            if(parameters.ContainsKey("current"))
            {
                MyItemSelected = (parameters["current"] as MyItem).Clone() as MyItem;
            }
        }

    }
}

