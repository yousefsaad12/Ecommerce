using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using Api.Helper;
using EcommerceApi.Models;

namespace Api.Interfaces
{
    public interface IProductInterface
    {
        public Task<List<Product>> GetAllProducts(QueryObject ?  queryObject);
        public Task<Product?> GetProductById(int prodId);
        public Task<bool> CreateProduct(Product prod);
        public Task<bool> UpdateProduct(Product prod, int prodId);
        public Task<bool> DeleteProduct(int prodId);
        public Task<ICollection<Product>> GetProductsWithIds(ICollection<OrderItemAddDTO> orderItems);

        public Task<bool>UpdateProductQuantity(Product product);
        
    }
}