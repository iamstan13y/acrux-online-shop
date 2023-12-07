using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Blazored.LocalStorage;

namespace AcruxShop.Web.Services;

public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IProductService _productService;

    private const string key = "ProductCollection";

    public ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService)
    {
        _localStorageService = localStorageService;
        _productService = productService;
    }

    public async Task<IEnumerable<ProductDto>> GetCollectionAsync() =>
        await _localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ??
        await AddCollectionAsync();

    public async Task RemoveCollection() =>
        await _localStorageService.RemoveItemAsync(key);

    private async Task<IEnumerable<ProductDto>> AddCollectionAsync()
    {
        var productCollection = await _productService.GetAllAsync();

        if (productCollection != null)
        {
            await _localStorageService.SetItemAsync(key, productCollection);
        }

        return productCollection;
    }
}