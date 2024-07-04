using System.Collections.Immutable;
using Api.Core.Domain;
using Api.Core.Dtos;
using Api.Core.Dtos.ProductDTOS;
using Api.Core.Helper;
using Api.Core.Models;
using Api.Core.ServicesInterfaces;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductInterface _productInterface;
        private readonly ICategoryInterface _categoryInterface;

        private readonly ICachingInterface _cachingInterface;
        public ProductController(IProductInterface productInterface, ICategoryInterface categoryInterface, ICachingInterface cachingInterface)
        {
            _productInterface = productInterface;
            _categoryInterface = categoryInterface;
            _cachingInterface = cachingInterface;
        }


        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult>GetAllProducts([FromQuery] QueryObject queryObject)
        {   
    

            var cacheData = _cachingInterface.GetData<IEnumerable<ProductResponseDTO>>("Products");


            if (cacheData != null && cacheData.Count() > 0)
               return Ok(cacheData);

            IEnumerable<Product> products  = await _productInterface.GetAllProducts(queryObject);
            cacheData = products.Select(p => p.ToProductResponseDTO());

            var expiryTime = DateTimeOffset.Now.AddSeconds(30);

            _cachingInterface.SetData<IEnumerable<ProductResponseDTO>>("Products", cacheData, expiryTime);
            
            return Ok(products.Select(p => p.ToProductResponseDTO()));
            
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

            if(await _productInterface.ProductExist(productCreate.Name) == true)
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
            
            return Ok(productCreated.ToProductResponseDTO());
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromQuery] int prodId, [FromBody] ProductUpdateDTO productUpdate)
        {
            if(productUpdate == null)
                return BadRequest();
                
            if(!ModelState.IsValid)
                return BadRequest();

            if(await _productInterface.ProductExist(productUpdate.Name) == true)
                return BadRequest("This product already exist");

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

            _cachingInterface.RemoveData($"Product{prodId}");

            return Ok("Product has been deleted");
        }



    }
}