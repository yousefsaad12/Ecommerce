using Api.Core.Domain;
using Api.Core.Dtos;
using Api.Core.Dtos.ProductDTOS;
using Api.Core.Helper;
using Api.Core.Models;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _productInterface;
        private readonly ICategoryInterface _categoryInterface;
        public ProductController(IProductInterface productInterface,  ICategoryInterface categoryInterface)
        {
            _productInterface = productInterface;
            _categoryInterface = categoryInterface;
        }


        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult>GetAllProducts([FromQuery] QueryObject queryObject)
        {
            List<Product> allProducts = await _productInterface.GetAllProducts(queryObject);

            var products = allProducts.Select(p => p.ToProductResponseDTO());
            
            return Ok(products);
            
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult>GetProduct([FromQuery] int prodId)
        {
            var product = await _productInterface.GetProductById(prodId);

            if(product == null)
               return BadRequest("Id is invalid");
            
            return Ok(product.ToProductResponseDTO());
        }

        [HttpPost]
        [Route("CreateProduct")]

        public async Task<IActionResult> CreateProducts([FromQuery]int categoryId, [FromBody] ProductCreateDTO productCreate)
        {   
            if(productCreate == null)
                return BadRequest();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var products = await _productInterface.GetAllProducts(null);

            var exist = products.Where(p => p.Name.Trim().ToUpper() == productCreate.Name.Trim().ToUpper())
                                .FirstOrDefault();

            if(exist != null)
                return BadRequest("This product already exist");

            var category = await _categoryInterface.GetCategoryById(categoryId);

            if(category == null)
                return BadRequest("This category does not exist");

            Product productCreated = productCreate.ToProduct(categoryId);

           if(!await _productInterface.CreateProduct(productCreated))
           {
                ModelState.AddModelError("", "Something went wrong while saving");
                return BadRequest(ModelState);
           }

            return Ok(productCreate);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int prodId, [FromBody] ProductUpdateDTO productUpdate)
        {
            if(productUpdate == null)
                return BadRequest();
                
            if(!ModelState.IsValid)
                return BadRequest();

            var products = await _productInterface.GetAllProducts(null);

            var exist = products.Where(p => p.Name.Trim().ToUpper() == productUpdate.Name.Trim().ToUpper())
                                .FirstOrDefault();

            if(exist != null)
                return BadRequest("This product already exist with this name");

            var category = await _categoryInterface.GetCategoryById(productUpdate.CategoryId);

            if(category == null)
                return BadRequest("This category does not exist");

            if(!await _productInterface.UpdateProduct(productUpdate.ToProductFromUpdate(),prodId))
            {
                ModelState.AddModelError("", "Error happen while saving");
                return BadRequest(ModelState);
            }

            return Ok(productUpdate);
        }

        [HttpDelete]
        [Route("DeleteProduct")]

        public async Task<IActionResult>DeleteProduct([FromQuery] int prodId)
        {
            var DeleteProduct = await _productInterface.GetProductById(prodId);

            if(DeleteProduct == null)
                return BadRequest("This product does not exist");

            if(!await _productInterface.DeleteProduct(prodId))
            {
                ModelState.AddModelError("", "Something happen while deleting");
                return BadRequest(ModelState);
            }

            return Ok("Product has been deleted");
        }



    }
}