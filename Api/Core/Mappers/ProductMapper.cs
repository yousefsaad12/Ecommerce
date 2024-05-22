using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.ProductDTOS;
using EcommerceApi.Models;

namespace Api.Mappers
{
    public static class  ProductMapper
    {
        public static ProductResponseDTO ToProductResponseDTO(this Product prod)
        {
            return new ProductResponseDTO()
            {
                id = prod.ProductId,
                Name = prod.Name,
                Description = prod.Description,
                Price = prod.Price,
                StockQuantity = prod.StockQuantity,
                CategoryName = prod.Category.Name,
            };
        }

        public static Product ToProduct(this ProductCreateDTO prod, int categoryId)
        {
            return new Product()
            {
                Name = prod.Name,
                Description = prod.Description,
                Price = prod.Price,
                StockQuantity = prod.StockQuantity,
                CategoryId = categoryId
            };
        }

         public static Product ToProductFromUpdate(this ProductUpdateDTO prod)
        {
            return new Product()
            {
                Name = prod.Name,
                Description = prod.Description,
                Price = prod.Price,
                StockQuantity = prod.StockQuantity,
                CategoryId = prod.CategoryId
            };
        }
    }
}