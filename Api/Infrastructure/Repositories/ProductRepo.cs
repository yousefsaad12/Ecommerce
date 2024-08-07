using Api.Core.Domain;
using Api.Core.Dtos.OrderItemDTO;
using Api.Core.Helper;
using Api.Core.Models;
using Api.Infrastructer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Api.Repositories
{
    public class ProductRepo : IProductInterface
    {
        private readonly ApplicationDbContext _context;

        public ProductRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProduct(Product prod)
        {
           await _context.AddAsync(prod);    

           return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<ICollection<Product>> GetProductsWithIds(ICollection<OrderItemAddDTO> orderItems)
        {   
            ICollection<Product> products = new List<Product>();

            foreach(var orderitem in orderItems)
            {
                products.Add(await GetProductById(orderitem.ProductId));
            }

            return products;
        }

        public async Task<bool> DeleteProduct(int prodId)
        {   
            Product ? removedProduct = await GetProductById(prodId); 

            if(removedProduct == null)
                return false;

            _context.Remove(removedProduct);

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Product>> GetAllProducts(QueryObject ? queryObject)
        {
            var productsQuery = _context.Products
                            .Include(p => p.Category)
                            .AsNoTracking() 
                            .AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(queryObject.ProductName))
                productsQuery = productsQuery.Where(p => p.Name.Contains(queryObject.ProductName));
            

            if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if (queryObject.SortBy.Equals("price", StringComparison.OrdinalIgnoreCase))
                {
                    productsQuery = queryObject.IsDecsending
                        ? productsQuery.OrderByDescending(p => p.Price)
                        : productsQuery.OrderBy(p => p.Price);
                }
            }

            
            var skipPages = (queryObject.PageNumber - 1) * queryObject.PageSize;

            
            productsQuery = productsQuery.Skip(skipPages).Take(queryObject.PageSize);

            return await productsQuery.ToListAsync();
        }


        public async Task<bool> ProductExist (string name)
        {   
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Name.Trim().ToUpper() == name.Trim().ToUpper());

           return product == null ? false : true; 
        }

        public async Task<Product?> GetProductById(int prodId)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.ProductId == prodId);
        }

        public async Task<bool> UpdateProduct(Product prod, int prodId)
        {
            Product ? updateProduct = await GetProductById(prodId);

            if(UpdateProduct == null)
                return false;

            updateProduct.Name = prod.Name;
            updateProduct.Price = prod.Price;
            updateProduct.Description = prod.Description;
            updateProduct.StockQuantity = prod.StockQuantity;
            updateProduct.CategoryId = prod.CategoryId;

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateProductQuantity(Product product)
        {
            _context.Products.Update(product);
           return await _context.SaveChangesAsync() > 0 ? true : false; 
        }

    }
}