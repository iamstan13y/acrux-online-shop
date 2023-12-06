using AcruxShop.API.Extensions;
using AcruxShop.API.Repositories.Contracts;
using AcruxShop.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AcruxShop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();

            var productDtos = products.ConvertToDto();

            return Ok(productDtos);
        }

        catch (Exception)
        {

            throw;
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        try
        {
            var product = await _productRepository.GetAsync(id);

            var productDto = product.ConvertToDto();

            return Ok(product);
        }

        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data!");
        }
    }

    [HttpGet]
    [Route(nameof(GetProductCategories))]
    public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
    {
        try
        {
            var productCategories = await _productRepository.GetCategoriesAsync();
            var productCategoryDtos = productCategories.ConvertToDto();

            return Ok(productCategoryDtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data!");
        }
    }

    [HttpGet]
    [Route("{categoryId}/GetItemsByCategory")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
    {
        try
        {
            var products = await _productRepository.GetItemsByCategoryAsync(categoryId);
            var productCategoryDtos = products.ConvertToDto();

            return Ok(productCategoryDtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data!");
        }
    }
}