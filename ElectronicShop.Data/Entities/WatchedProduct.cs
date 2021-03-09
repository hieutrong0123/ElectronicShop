using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class WatchedProduct
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public Product Product { get; set; }
    }
}
