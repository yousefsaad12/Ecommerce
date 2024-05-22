using Api.Core.Dtos.CategoryDTOS;
using Api.Core.Models;

namespace Api.Mappers
{
    public static class CategoryMapper
    {
        
        public static CategoryResponseDTO ToCategoryResponseDTO(this Category cat)
        {
            return new CategoryResponseDTO()
            {
                Name = cat.Name,
                id = cat.CategoryId,
            };
        }

        public static Category ToCategory(this CategoryCreateDTO cat)
        {
            return new Category()
            {
                Name = cat.Name,
            };
        }
    }
}