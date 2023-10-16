using AcruxShop.API.Entities;
using AcruxShop.Models.Dtos;

namespace AcruxShop.API.Repositories.Contracts;

public interface IShoppingCartRepository
{
    Task<CartItem> AddItemAsync(CartItemToAddDto cartItemToAddDto);
    Task<CartItem> UpdateQuantityAsync(int id, CartItemQuantityUpdateDto quantityUpdateDto);
    Task<CartItem> DeleteItemAsync(int id);
    Task<CartItem> GetItemAsync(int id);
    Task<List<CartItem>> GetItemsAsync(int userId);
}