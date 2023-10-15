using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class ShoppingCartBase : ComponentBase
{
    [Inject]
    public IShoppingCartService? ShoppingCartService { get; set; }
    public IEnumerable<CartItemDto>? CartItems { get; set; }
	public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
		try
		{
			CartItems = await ShoppingCartService!.GetCartItemsAsync(HardCoded.UserId);
		}
		catch (Exception ex)
		{
		 ErrorMessage = ex.Message;
		}
    }
}