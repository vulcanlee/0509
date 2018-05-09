using System;
using System.Collections.Generic;
using System.Text;

namespace XFListView.Models
{
    public class MyItem : ICloneable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
