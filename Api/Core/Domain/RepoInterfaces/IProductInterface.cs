using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Helper;
using Api.Core.Models;

namespace Api.Core.Domain
{
    public interface IProductInterface
    {
        public Task<List<Product>> GetAllProducts(QueryObject ?  queryObject);
        public Task<Product?> GetProductById(int prodId);
        public Task<bool> CreateProduct(Product prod);
        public Task<bool> UpdateProduct(Product prod, int prodId);
        public Task<bool> DeleteProduct(int prodId);
        public Task<ICollection<Product>> GetProductsWithIds(ICollection<OrderItemAddDTO> orderItems);
        public Task<bool> ProductExist (string name);
        public Task<bool>UpdateProductQuantity(Product product);
        
    }
}