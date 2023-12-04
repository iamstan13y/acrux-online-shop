using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class ProductsByCategoryBase : ComponentBase
{
    [Parameter]
    public int CategoryId { get; set; }
    [Inject]
    public IProductService ProductService { get; set; }
    public IEnumerable<ProductDto> Products { get; set; }
    public string CategoryName { get; set; }
    public string ErrorMessage { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Products = await ProductService.GetItemsByCategory(CategoryId);

            if (Products != null && Products.Any())
            {
                var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);
                if (productDto != null)
                {
                    CategoryName = productDto.CategoryName;
                }
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}