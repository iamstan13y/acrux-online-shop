using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Blazored.LocalStorage;

namespace AcruxShop.Web.Services;

public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IShoppingCartService _shoppingCartService;

    private const string key = "CartItemCollection";

    public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService, IShoppingCartService shoppingCartService)
    {
        _localStorageService = localStorageService;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<List<CartItemDto>> GetCollectionAsync() => 
        await _localStorageService.GetItemAsync<List<CartItemDto>>(key) ??
        await AddCollectionAsync();

    public async Task RemoveCollection() => await _localStorageService.RemoveItemAsync(key);

    public async Task SaveCollection(List<CartItemDto> cartItemDtos) => await _localStorageService.SetItemAsync(key, cartItemDtos);

    private async Task<List<CartItemDto>> AddCollectionAsync()
    {
        var shoppingCartCollection = await _shoppingCartService.GetCartItemsAsync(HardCoded.UserId);

        if (shoppingCartCollection != null)
        {
            await _localStorageService.SetItemAsync(key, shoppingCartCollection);
        }

        return (List<CartItemDto>)shoppingCartCollection;
    }
}