using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
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
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");

            return products;
        }
        catch (Exception)
        {

            throw;
        }
    }
}