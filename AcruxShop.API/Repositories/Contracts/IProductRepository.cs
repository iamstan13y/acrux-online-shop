using AcruxShop.API.Entities;

namespace AcruxShop.API.Repositories.Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<ProductCategory>> GetCategoriesAsync();
    Task<Product> GetAsync(int id);
    Task<ProductCategory> GetCategoryAsync(int id);
    Task<IEnumerable<Product>> GetItemsByCategoryAsync(int categoryId);
}