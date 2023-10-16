using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class ShoppingCartBase : ComponentBase
{
    [Inject]
    public IShoppingCartService? ShoppingCartService { get; set; }
    public List<CartItemDto>? CartItems { get; set; }
	public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
		try
		{
			CartItems = (await ShoppingCartService!.GetCartItemsAsync(HardCoded.UserId)).ToList();
		}
		catch (Exception ex)
		{
		 ErrorMessage = ex.Message;
		}
    }

	protected async Task DeleteCartItem_Click(int id)
	{
		var cartitemDto = await ShoppingCartService.DeleteItemAsync(id);

		RemoveCartItem(id);
	}

    private CartItemDto GetCartItem(int id) => CartItems?.FirstOrDefault(i => i.Id == id)!;

    private void RemoveCartItem(int id)
	{
		var cartItemDto = GetCartItem(id);

		CartItems?.Remove(cartItemDto);
	}
}