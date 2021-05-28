using ElectronicShop.Application.Categories.Commands;
using ElectronicShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Application.Categories.Mapper
{
    public static class CategoryMapper
    {
        public static void Map(this Category category, UpdateCategoryCommand request)
        {
            category.Name = request.Name;

            category.Alias = request.Alias;

            category.RootId = request.RootId;

            category.ProductTypeId = request.ProductTypeId;

            category.DateModified = DateTime.Now;
        }
    }
}
