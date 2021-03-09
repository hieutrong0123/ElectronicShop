using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class ProductType
    {
        public int Id { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public List<Category> Categories { get; set; }
    }
}
