using System;
using System.Collections.Generic;
using System.Text;
using XFListView.Models;

namespace XFListView.Services
{
    public class MyTaskService
    {
        public static int LastID = 0;
        public static List<MyItem> GetAllTask()
        {
            List<MyItem> fooMyItems =
                new List<MyItem>();
            for (int i = 0; i < 10; i++)
            {
                fooMyItems.Add(new MyItem()
                {
                    Id = LastID,
                    Name = $"工作 {LastID}",
                    Status = (LastID % 3) == 0 ? 
                    "尚未開始" : "已完成",
                    Date = DateTime.Now.AddDays(LastID),                    
                });
                LastID++;
            }
            return fooMyItems;
        }
    }
}
