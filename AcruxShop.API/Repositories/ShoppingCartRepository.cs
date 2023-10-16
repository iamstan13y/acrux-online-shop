using AcruxShop.API.Data;
using AcruxShop.API.Entities;
using AcruxShop.API.Repositories.Contracts;
using AcruxShop.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AcruxShop.API.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly AppDbContext _context;

    public ShoppingCartRepository(AppDbContext context) => _context = context;

    private async Task<bool> CartItemExistsAsync(int cartId, int productId) =>
        await _context.CartItems!.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);

    public async Task<CartItem> AddItemAsync(CartItemToAddDto cartItemToAddDto)
    {
        if (await CartItemExistsAsync(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
        {
            var item = await (from product in _context.Products
                              where product.Id == cartItemToAddDto.ProductId
                              select new CartItem
                              {
                                  CartId = cartItemToAddDto.CartId,
                                  ProductId = product.Id,
                                  Quantity = cartItemToAddDto.Quantity
                              }).SingleOrDefaultAsync();

            if (item != null)
            {
                var result = await _context.CartItems!.AddAsync(item);
                await _context.SaveChangesAsync();

                return result.Entity;
            }
        }

        return null;
    }

    public Task<CartItem> DeleteItemAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItem> GetItemAsync(int id)
    {
        return await (from cart in _context.Carts
                      join cartItem in _context.CartItems
                      on cart.Id equals cartItem.CartId
                      where cartItem.Id == id
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          ProductId = cartItem.ProductId,
                          Quantity = cartItem.Quantity,
                          CartId = cartItem.CartId
                      }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CartItem>> GetItemsAsync(int userId)
    {
        return await (from cart in _context.Carts
                      join cartItem in _context.CartItems!
                      on cart.Id equals cartItem.CartId
                      where cart.UserId == userId
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          ProductId = cartItem.ProductId,
                          Quantity = cartItem.Quantity,
                          CartId = cartItem.CartId
                      }).ToListAsync();
    }

    public Task<CartItem> UpdateQuantityAsync(int id, CartItemQuantityUpdateDto quantityUpdateDto)
    {
        throw new NotImplementedException();
    }
}