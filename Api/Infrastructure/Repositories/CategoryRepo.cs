using Api.Infrastructer.Data;
using Microsoft.EntityFrameworkCore;
using Api.Core.Models;
using Api.Core.Domain;

namespace Api.Repositories
{
    public class CategoryRepo : ICategoryInterface
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateCategory(Category cat)
        {
           await _context.AddAsync(cat);

           return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> DeleteCategory(int catId)
        {
            Category ? deletedCategory = await GetCategoryById(catId); 

            if(deletedCategory == null)
                return false;

            _context.Remove(deletedCategory);

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int catId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == catId);
        }

        public async Task<List<Product>> GetProductOfCategory(int catId)
        {
            return await _context.Products.Where(p => p.CategoryId == catId).ToListAsync();
        }

        public async Task<bool> UpdateCategory(Category cat, int catId)
        {
            Category ? updatedCategory = await GetCategoryById(catId);

            if(updatedCategory == null)
                return false;

            updatedCategory.Name = cat.Name;

            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool>CategoryExist(string name)
        {
           var exist = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Trim().ToUpper() == name.Trim().ToUpper());

           return exist == null ? false : true;
        }
    }
}