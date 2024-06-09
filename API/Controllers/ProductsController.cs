

using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Products> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        
        public ProductsController(IGenericRepository<Products> productsRepo, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;
            
        }

        [HttpGet]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<Pagination<IReadOnlyList<ProductToReturnDto>>>> GetProducts(
            [FromQuery]ProductSpecParams productParams) 
        {

            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

            var countSpec = new ProductWithFiltersForCountSpecification(productParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);

            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper
                .Map<IReadOnlyList<Products>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, 
            productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id) 
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id); 
            var product = await _productsRepo.GetEntityWithSpec(spec);

            if (product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Products, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productBrandRepo.ListAllAsync());
        }

         [HttpGet("types")]
         [EnableCors("CorsPolicy")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productTypeRepo.ListAllAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [EnableCors("CorsPolicy")]
        public async Task<ActionResult<Products>> AddProduct(Products product)
        {
            _productsRepo.Add(product);
            await _productsRepo.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> UpdateProduct(int id, Products product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _productsRepo.Update(product);
            await _productsRepo.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [EnableCors("CorsPolicy")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productsRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _productsRepo.Delete(product);
            await _productsRepo.SaveChangesAsync();

            return NoContent();
        }





    }
}