using AcruxShop.Models.Dtos;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class DisplayProductBase : ComponentBase
{

    [Parameter]
    public IEnumerable<ProductDto> Products { get; set; }
}