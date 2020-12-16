using System;
using System.Collections.Generic;

#nullable disable

namespace VirtualClinic.DataModel
{
    public partial class Item
    {
        public Item()
        {
            Invintories = new HashSet<Invintory>();
            OrderItems = new HashSet<OrderItem>();
        }

        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }

        public virtual ICollection<Invintory> Invintories { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
