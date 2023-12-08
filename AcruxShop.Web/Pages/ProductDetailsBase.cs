using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class ProductDetailsBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IProductService? ProductService { get; set; }
    [Inject]
    public IShoppingCartService? ShoppingCartService { get; set; }
    [Inject]
    public IManageCartItemsLocalStorageService? ManageCartItemsLocalStorageService { get; set; }
    [Inject]
    public IManageProductsLocalStorageService? ManageProductsLocalStorageService { get; set; }
    private List<CartItemDto>? CartItems { get; set; }
    [Inject]
    public NavigationManager? NavigationManager { get; set; }
    public ProductDto? Product { get; set; }
    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            CartItems = (await ManageCartItemsLocalStorageService.GetCollectionAsync()).ToList();
            Product = await GetProductByIdAsync(Id);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var cartItemDto = await ShoppingCartService!.AddItemAsync(cartItemToAddDto);
            if (cartItemDto == null)
            {
                CartItems.Add(cartItemDto);
                await ManageCartItemsLocalStorageService.SaveCollection(CartItems);
            }

            NavigationManager?.NavigateTo("/ShoppingCart");
        }
        catch (Exception)
        {

            throw;
        }
    }

    private async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var productDtos = await ManageProductsLocalStorageService.GetCollectionAsync();

        if (productDtos != null)
        {
            return productDtos.SingleOrDefault(p => p.Id == id);
        }
        return null;
    }
}