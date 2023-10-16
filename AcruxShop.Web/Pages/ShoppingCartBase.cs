using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcruxShop.Web.Pages;

public class ShoppingCartBase : ComponentBase
{
	[Inject]
	public IJSRuntime JS { get; set; }
    [Inject]
    public IShoppingCartService? ShoppingCartService { get; set; }
    public List<CartItemDto>? CartItems { get; set; }
	protected string? TotalPrice { get; set; }
	protected int TotalQuantity { get; set; }
	public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
		try
		{
			CartItems = (await ShoppingCartService!.GetCartItemsAsync(HardCoded.UserId)).ToList();
			CalculateCartSummaryTotals();
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
        CalculateCartSummaryTotals();
    }

	protected async Task UpdateCartItenQuantity_Click(int id, int quantity)
	{
		try
		{
			if (quantity > 0)
			{
				var updateItemDto = new CartItemQuantityUpdateDto
				{
					CartItemId = id,
					Quantity = quantity
				};

				var returnedUpdateItemDto = await ShoppingCartService.UpdateQuantityAsync(updateItemDto);
				UpdateItemTotalPrice(returnedUpdateItemDto);

				CalculateCartSummaryTotals();

				await MakeQuantityButtonVisible(id, false);
            }
			else
			{
				var item = CartItems?.FirstOrDefault(x => x.Id == id);
				if (item != null)
				{
					item.Quantity = 1;
					item.Total = item.Price;
				}
			}
		}
		catch (Exception)
		{

			throw;
		}
	}

	protected async Task UpdateQuantity_Input(int id)
	{
		await JS.InvokeVoidAsync("MakeQuantityButtonVisible", id, true);
	}

    private async Task MakeQuantityButtonVisible(int id, bool visible)
    {
        await JS.InvokeVoidAsync("MakeQuantityButtonVisible", id, visible);
    }

    private void UpdateItemTotalPrice(CartItemDto cartItemDto)
	{
		var item = GetCartItem(cartItemDto.Id);

		if (item != null)
		{
			item.Total = cartItemDto.Price * cartItemDto.Quantity;
		}
	}

	private void CalculateCartSummaryTotals()
	{
		SetTotalPrice();
		SetTotalQuantity();
	}

	private void SetTotalPrice()
	{
		TotalPrice = CartItems.Sum(p => p.Total).ToString("C");
	}

    private void SetTotalQuantity()
    {
		TotalQuantity = CartItems.Sum(p => p.Quantity);
    }

    private CartItemDto GetCartItem(int id) => CartItems?.FirstOrDefault(i => i.Id == id)!;

    private void RemoveCartItem(int id)
	{
		var cartItemDto = GetCartItem(id);

		CartItems?.Remove(cartItemDto);
	}
}