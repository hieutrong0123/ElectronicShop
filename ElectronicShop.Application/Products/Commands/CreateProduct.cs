using ElectronicShop.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Application.Products.Commands
{
    public class CreateProduct
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Specifications { get; set; }

        public string Description { get; set; }

        public int GoodsReceipt { get; set; }

        public int Inventory { get; set; }

        public ProductStatus Status { get; set; }

        public int CategoryId { get; set; }

        public string Alias { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string CreatedBy { get; set; }

        public string ModefiedBy { get; set; }
    }
}
