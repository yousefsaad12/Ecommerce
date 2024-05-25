using Api.Core.Domain;
using Api.Core.Dtos.CategoryDTOS;
using Api.Core.Models;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryInterface _categoryInterface;

        public CategoryController(ICategoryInterface categoryInterface)
       {
            _categoryInterface = categoryInterface;
       }

       [HttpGet]
       [Route("GetAllCategories")]
       public async Task<IActionResult> GetAllCategories()
       {    
          var categories = await _categoryInterface.GetAllCategories();

          IEnumerable<CategoryResponseDTO> categoryResponseDTOs = categories.Select(c => c.ToCategoryResponseDTO());
          
          return Ok(categoryResponseDTOs);
       }

       [HttpGet]
       [Route("GetCategory")]
       public async Task<IActionResult> GetCategory([FromQuery]int categoryId)
       { 
          Category ? category = await _categoryInterface.GetCategoryById(categoryId);

          if(category == null)
               return BadRequest("this category not exist");
          
          return Ok(category.ToCategoryResponseDTO());
       }

       [HttpGet]
       [Route("GetProductsOfCategory")]
       public async Task<IActionResult> GetProductsOfCategory([FromQuery]int categoryId)
       { 
          var exist = await _categoryInterface.GetCategoryById(categoryId);

          if(exist == null)
               return BadRequest("This category not exist");

          var products = await _categoryInterface.GetProductOfCategory(categoryId);

          return Ok(products.Select(p => p.ToProductResponseDTO()));
       }


       [HttpPost]
       [Route("CreateCategory")]
       public async Task<IActionResult> CreateCategory([FromBody]CategoryCreateDTO categoryCreate)
       {
          if(!ModelState.IsValid)
               return BadRequest("Name must be between 3 to 75 char");

          var categories = await _categoryInterface.GetAllCategories();

          var exist = categories.Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.Trim().ToUpper()).FirstOrDefault();

          if(exist != null)
               return  BadRequest("Category is already exist");



          if(!await _categoryInterface.CreateCategory(categoryCreate.ToCategory()))
          {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
          }

          return Ok(categoryCreate);

       }

         
       [HttpPut]
       [Route("UpdateCategory")]

       public async Task<IActionResult> UpdateCategory([FromQuery] int catId, [FromBody] CategoryCreateDTO categoryUpdate)
       {
           if(!ModelState.IsValid)
               return BadRequest("Name must be between 3 to 75 char");

          var categories = await _categoryInterface.GetAllCategories();

          var exist = categories.Where(c => c.Name.Trim().ToUpper() == categoryUpdate.Name.Trim().ToUpper()).FirstOrDefault();

          if(exist != null)
               return BadRequest("Category is already exist");

          if(!await _categoryInterface.UpdateCategory(categoryUpdate.ToCategory(), catId))
          {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
          }

          return Ok(categoryUpdate);
       }

       [HttpDelete]
       [Route("DeleteCategory")]

       public async Task<IActionResult> DeleteCategory([FromQuery] int catId)
       {  
          if(!await _categoryInterface.DeleteCategory(catId))
               return BadRequest("This category not exist");

       

          return Ok("Category has been deleted");
          
       }


    }
}