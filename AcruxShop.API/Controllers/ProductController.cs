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
            var productCategories = await _productRepository.GetCategoriesAsync();

            var productDtos = products.ConvertToDto(productCategories);

            return Ok(productDtos);
        }

        catch (Exception)
        {

            throw;
        }
    }
}