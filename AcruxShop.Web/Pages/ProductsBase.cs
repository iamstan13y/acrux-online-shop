﻿using AcruxShop.Models.Dtos;
using AcruxShop.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace AcruxShop.Web.Pages;

public class ProductsBase : ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; }
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; }

    public IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ProductService.GetAllAsync();

        var cartItems = await ShoppingCartService.GetCartItemsAsync(HardCoded.UserId);
        var totalQuantity = cartItems.Sum(x => x.Quantity);

        ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQuantity);
    }

    protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
    {
        return from product in Products
               group product by product.CategoryId into prodByCatGroup
               orderby prodByCatGroup.Key
               select prodByCatGroup;
    }

    protected string GetCategoryName(IGrouping<int, ProductDto> groupedProducts)
    {
        return groupedProducts.FirstOrDefault(pg => pg.CategoryId == groupedProducts.Key).CategoryName;
    }

}