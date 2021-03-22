using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int? RootId { get; set; }

        public int ProductTypeId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public List<Product> Products { get; set; }

        public ProductType ProductType { get; set; }

        public Category Parent { get; set; }

        public List<Category> Children { get; set; }

        public AspNetUser AspNetUser { get; set; }
    }
}
