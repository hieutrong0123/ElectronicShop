using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public List<Order> Orders { get; set; }

        public List<OrderStatusDetail> OrderStatusDetails { get; set; }
    }
}
