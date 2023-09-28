using AcruxShop.API.Entities;
using AcruxShop.API.Repositories.Contracts;
using AcruxShop.Models.Dtos;

namespace AcruxShop.API.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    public Task<CartItem> AddItemAsync(CartItemToAddDto cartItemToAddDto)
    {
        throw new NotImplementedException();
    }

    public Task<CartItem> DeleteItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CartItem> GetItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CartItem>> GetItemsAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<CartItem> UpdateQuantityAsync(int id, CartItemQuantityUpdateDto quantityUpdateDto)
    {
        throw new NotImplementedException();
    }
}