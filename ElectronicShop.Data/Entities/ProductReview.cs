using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class ProductReview
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        
        public int ProductId { get; set; }

        public double RateStar { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Status { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public Product Product { get; set; }
    }
}
