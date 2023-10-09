using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarketBackend.Data;
using SuperMarketBackend.DTO;
using SuperMarketBackend.Services;
using Microsoft.AspNetCore.Authorization;

namespace SuperMarketBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductServices _productServices;
        private readonly IMapper _mapper;
        public ProductsController(IMapper mapper)
        {
            _productServices = new ProductServices();
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDTO product)
        {
            var data = _productServices.AddProduct(_mapper.Map<Product>(product));
            if (data != null)
                return Ok(data);
            else
                return BadRequest(new ProductDTO() { ProductName = "Something went wrong" });
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDTO product)
        {
            var data = _productServices.UpdateProduct(_mapper.Map<Product>(product));
            if (data != null)
                return Ok(data);
            else
                return BadRequest("Something went wrong");
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var data = _productServices.DeleteProduct(id);
            if (data)
                return Ok(data);
            else
                return BadRequest(data);
        }


        [HttpGet]
        [Authorize]
        [Route("GetProduct/{id?}/{productName?}")]
        public async Task<IActionResult> GetProduct(int id = 0,string? productName = "")
        {
            var data = _productServices.GetProduct(id, productName);
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllProducts/{id?}/{search?}/{orderBydesc?}")]
        public async Task<IActionResult> GetAllProducts(int id = 0,string? search = "",bool orderBydesc = false)
        {
            var data = _productServices.GetAllProducts(id,search,orderBydesc);
            if(data != null)
                return Ok(data);
            else
                return NotFound();
        }

        [HttpGet]
        [Authorize]
        [Route("GetAllProductsCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var data = _productServices.GetAllProductCategories();
            if (data != null)
                return Ok(data);
            else
                return NotFound();
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteProductCategory/{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var data = _productServices.DeleteProductCategory(id);
            if (data)
                return Ok(data);
            else
                return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [Route("AddProductCategory")]
        public async Task<IActionResult> AddProductCategory(ProductCategoryDTO category)
        {
            var data = _productServices.AddProductCategory(_mapper.Map<ProductCategory>(category));
            if (data != null)
                return Ok(data);
            else
                return BadRequest(new ProductCategoryDTO() { CategoryName = "Error..Category Not Saved!!"});
        }
    }
}
