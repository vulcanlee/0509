﻿using Prism.Commands;
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
        public DelegateCommand AddCommand { get; set; }
        public bool EditMode { get; set; }
        public bool SaveData { get; set; }
        public bool ShowEditMode { get; set; }
        public bool ShowAddMode { get; set; }
        public DetailPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            AddCommand = new DelegateCommand(() =>
            {
                SaveData = true;
                NavigationParameters para = new NavigationParameters();
                para.Add("current", MyItemSelected);
                para.Add("SaveData", SaveData);
                para.Add("EditMode", false);
                _navigationService.GoBackAsync(para);
            });

            SaveCommand = new DelegateCommand(() =>
            {
                SaveData = true;
                NavigationParameters para = new NavigationParameters();
                para.Add("current", MyItemSelected);
                para.Add("SaveData", SaveData);
                para.Add("EditMode", EditMode);
                _navigationService.GoBackAsync(para);
            });
            DeleteCommand = new DelegateCommand(() =>
            {
                SaveData = false;
                NavigationParameters para = new NavigationParameters();
                para.Add("current", MyItemSelected);
                para.Add("SaveData", SaveData);
                para.Add("EditMode", EditMode);
                _navigationService.GoBackAsync(para);
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
            if (parameters.ContainsKey("current"))
            {
                MyItemSelected = (parameters["current"] as MyItem).Clone() as MyItem;
            }
            if (parameters.ContainsKey("EditMode"))
            {
                EditMode = (bool)parameters["EditMode"];
                if(EditMode == true)
                {
                    // show save and delete button
                    ShowEditMode = true;
                    ShowAddMode = false;
                }
                else
                {
                    // show save button for Add new Item
                    ShowEditMode = false;
                    ShowAddMode = true;
                }
            }
        }

    }
}

