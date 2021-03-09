using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DeliveryDate  { get; set; }

        public bool Paid { get; set; }

        public string Receiver { get; set; }

        public string ReceiversAddress { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalMoney { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public OrderStatus OrderStatus { get; set; }
      
        public List< OrderDetail > OrderDetails { get; set; }

        public List<OrderStatusDetail> OrderStatusDetails { get; set; }
    }
}
