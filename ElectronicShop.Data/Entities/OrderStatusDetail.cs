using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class OrderStatusDetail
    {
        public int Id { get; set; }

        public int StatusId { get; set; }

        public int OrderId { get; set; }

        public DateTime DateCreated { get; set; }

        public Order Order { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }
}
