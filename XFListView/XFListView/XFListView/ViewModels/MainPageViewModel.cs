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
        public bool IsRefreshing { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
         public DelegateCommand<MyItem> DeleteCommand { get; set; }
       public bool EditMode { get; set; }
        public DelegateCommand AddCommand { get; set; }
        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            TapCommand = new DelegateCommand(() =>
            {
                NavigationParameters para = new NavigationParameters();
                para.Add("current", MyTaskSelected);
                EditMode = true;
                para.Add("EditMode", EditMode);
                _navigationService.NavigateAsync("DetailPage", para);
            });
            RefreshCommand = new DelegateCommand(() =>
            {
                Refresh();
                IsRefreshing = false;
            });
            AddCommand = new DelegateCommand(() =>
            {
                var fooNewTask = new MyItem()
                {
                    Date = DateTime.Now.AddDays(-3),
                    Status = "還沒有"
                };
                NavigationParameters para = new NavigationParameters();
                para.Add("current", fooNewTask);
                EditMode = false;
                para.Add("EditMode", EditMode);
                _navigationService.NavigateAsync("DetailPage", para);
            });
            DeleteCommand = new DelegateCommand<MyItem>((x) =>
            {
                MyTasks.Remove(x);
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
            //para.Add("current", MyItemSelected);
            //para.Add("SaveData", SaveData);
            //para.Add("EditMode", EditMode);
            if (parameters.ContainsKey("EditMode"))
            {
                EditMode = (bool)parameters["EditMode"];

                if (parameters.ContainsKey("current"))
                {
                    var fooUpdateItem = parameters["current"] as MyItem;
                    if (parameters.ContainsKey("SaveData"))
                    {
                        var fooSaveData = (bool)parameters["SaveData"];

                        // 開始進行資料的 CRUD 處理
                        if (EditMode == true)
                        {
                            if (fooSaveData == true)
                            {
                                var fooItem = MyTasks.FirstOrDefault(x => x.Id == fooUpdateItem.Id);
                                if (fooItem != null)
                                {
                                    fooItem.Name = fooUpdateItem.Name;
                                    fooItem.Status = fooUpdateItem.Status;
                                    fooItem.Date = fooUpdateItem.Date;
                                }
                            }
                            else
                            {
                                var fooItem = MyTasks.FirstOrDefault(x => x.Id == fooUpdateItem.Id);
                                if (fooItem != null)
                                {
                                    MyTasks.Remove(fooItem);
                                }
                            }
                        }
                        else
                        {
                            // Add New Item
                            MyTasks.Insert(0, fooUpdateItem);
                        }
                    }

                }

            }

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
