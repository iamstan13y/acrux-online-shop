﻿using AcruxShop.Models.Dtos;

namespace AcruxShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetAsync(int id);
    Task<IEnumerable<ProductCategoryDto>> GetProductCategories();
    Task<IEnumerable<ProductDto>> GetItemsByCategory(int categoryId);
}