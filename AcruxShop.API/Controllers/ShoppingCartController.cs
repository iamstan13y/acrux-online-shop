using AcruxShop.API.Extensions;
using AcruxShop.API.Repositories.Contracts;
using AcruxShop.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AcruxShop.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IProductRepository _productRepository;

    public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _productRepository = productRepository;
    }

    [HttpGet("items/{userId}")]
    public async Task<ActionResult<IEnumerable<CartItemDto>>> Get(int userId)
    {
        try
        {
            var cartItems = await _shoppingCartRepository.GetItemsAsync(userId);

            if (cartItems == null) return NoContent();

            var products = await _productRepository.GetAllAsync() ?? throw new Exception("No products in system.");

            var cartItemsDto = cartItems.ConvertToDto(products);

            return Ok(cartItemsDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Some error occured: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartItemDto>> GetItem(int id)
    {
        try
        {
            var cartItem = await _shoppingCartRepository.GetItemAsync(id);
            if (cartItem == null) return NotFound();

            var product = await _productRepository.GetAsync(cartItem.ProductId);
            if (product == null) return NotFound();

            var cartItemDto = cartItem.ConvertToDto(product);

            return Ok(cartItemDto);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Some error occured: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<CartItemDto>> Post(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var newCartItem = await _shoppingCartRepository.AddItemAsync(cartItemToAddDto);
            if (newCartItem == null) return NoContent();

            var product = await _productRepository.GetAsync(cartItemToAddDto.ProductId) ?? 
                throw new Exception($"Something went wrong attempting to retrieve the product: {newCartItem.ProductId}");

            var newCartItemDto = newCartItem.ConvertToDto(product);

            return CreatedAtAction(nameof(GetItem), new { id = newCartItemDto.Id }, newCartItemDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Some error occured: {ex.Message}");
        }
    }
}