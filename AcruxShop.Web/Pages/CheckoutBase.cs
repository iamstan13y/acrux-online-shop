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
        [Inject]
        public IManageCartItemsLocalStorageService? ManageCartItemsLocalStorageService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                CartItems = await ManageCartItemsLocalStorageService.GetCollectionAsync();

                if (CartItems != null)
                {
                    Guid orderGuid = Guid.NewGuid();
                    PaymentAmount = CartItems.Sum(p => p.Total);
                    TotalQuantity = CartItems.Sum(p => p.Quantity);
                    PaymentDescription = $"0_{HardCoded.UserId}_{orderGuid}";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                if (firstRender)
                {
                    await Js.InvokeVoidAsync("initPayPalButton");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}