using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace AcruxShop.Web.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(HttpClient httpClient)
    {
            _httpClient = httpClient;
    }

    public async Task<CartItemDto> AddItemAsync(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/v1/ShoppingCart", cartItemToAddDto);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default!;
                }

                return (await response.Content.ReadFromJsonAsync<CartItemDto>())!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"HTTP Status:{response.StatusCode} Message: -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/v1/ShoppingCart/items/{userId}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CartItemDto>();
                }

                return (await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>())!;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"HTTP Status:{response.StatusCode} Message: -{message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}