using AcruxShop.API.Entities;
using AcruxShop.Models.Dtos;

namespace AcruxShop.API.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
    {
        return (from  productCategory in productCategories
                select new ProductCategoryDto
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name,
                    IconCSS = productCategory.IconCSS,
                }).ToList();
    }

    public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
    {
        return (from product in products
                select new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CategoryId = product.ProductCategory.Id,
                    CategoryName = product.ProductCategory.Name
                }).ToList();
    }

    public static ProductDto ConvertToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            Quantity = product.Quantity,
            CategoryId = product.ProductCategory.Id,
            CategoryName = product.ProductCategory.Name
        };
    }

    public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
    {
        return (from cartItem in cartItems
                join product in products
                on cartItem.ProductId equals product.Id
                select new CartItemDto
                {
                    CartId = cartItem.CartId,
                    ProductId = cartItem.ProductId,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Id = cartItem.Id,
                    Quantity = cartItem.Quantity,
                    Total = product.Price * cartItem.Quantity,
                }).ToList();
    }

    public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
    {
        return new CartItemDto
        {
            CartId = cartItem.CartId,
            ProductId = cartItem.ProductId,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductImageUrl = product.ImageUrl,
            Price = product.Price,
            Id = cartItem.Id,
            Quantity = cartItem.Quantity,
            Total = product.Price * cartItem.Quantity,
        };
    }
}