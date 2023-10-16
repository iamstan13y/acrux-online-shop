using AcruxShop.Models.Dtos;

namespace AcruxShop.Web.Services.Contracts;

public interface IShoppingCartService
{
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);
    Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto);
    Task<CartItemDto> DeleteItemAsync(int id);
}