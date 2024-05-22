using Api.Core.Models;

namespace Api.Interfaces
{
    public interface ICategoryInterface
    {
        public Task<List<Category>> GetAllCategories();
        public Task<Category?> GetCategoryById(int catId);
        public Task<List<Product>> GetProductOfCategory(int catId);
        public Task<bool> CreateCategory(Category cat);
        public Task<bool> UpdateCategory(Category cat, int catId);
        public Task<bool> DeleteCategory(int catId);
    }
}