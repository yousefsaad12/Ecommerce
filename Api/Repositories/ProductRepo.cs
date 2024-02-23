using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.OrderItemDTO;
using Api.Interfaces;
using EcommerceApi.Data;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int prodId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == prodId);
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
    }
}