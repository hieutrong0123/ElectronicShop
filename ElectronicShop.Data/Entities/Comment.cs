using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int? ParentId { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public AspNetUser AspNetUser { get; set; }

        public Product Product { get; set; }

        public Comment Parent { get; set; }

        public List<Comment> Children { get; set; }
    }
}
