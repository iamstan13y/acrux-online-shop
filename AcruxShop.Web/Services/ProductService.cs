using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace AcruxShop.Web.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/v1/Product");

            return products;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ProductDto> GetAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/v1/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default(ProductDto);
                }

                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception(message);
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}