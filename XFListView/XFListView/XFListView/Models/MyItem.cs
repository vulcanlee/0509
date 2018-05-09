using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XFListView.Models
{
    public class MyItem : ICloneable, INotifyPropertyChanged
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
