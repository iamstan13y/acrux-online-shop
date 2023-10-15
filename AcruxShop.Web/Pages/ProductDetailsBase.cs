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
    public NavigationManager? NavigationManager { get; set; }
    public ProductDto? Product { get; set; }
    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Product = await ProductService!.GetAsync(Id);
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
            NavigationManager?.NavigateTo("/ShoppingCart");
        }
        catch (Exception)
        {

            throw;
        }
    }
}