using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace AcruxShop.Web.Pages
{
    public class CheckoutBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get; set; }
        protected IEnumerable<CartItemDto> CartItems { get; set; }
        protected int TotalQuantity { get; set; }
        protected string PaymentDescription { get; set; }
        protected decimal PaymentAmount { get; set; }
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ShoppingCartService.GetCartItemsAsync(HardCoded.UserId);

                if (CartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();
                    PaymentAmount = CartItems.Sum
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}