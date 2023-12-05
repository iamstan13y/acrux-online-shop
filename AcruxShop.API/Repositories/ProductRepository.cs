using AcruxShop.API.Data;
using AcruxShop.API.Entities;
using AcruxShop.API.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AcruxShop.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _context.Products!.ToListAsync();
        return products;
    }

    public async Task<Product> GetAsync(int id)
    {
        var product = await _context.Products!.FindAsync(id);
        return product;
    }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
    {
        var categories = await _context.ProductCategories!.ToListAsync();
        return categories;
    }

    public async Task<ProductCategory> GetCategoryAsync(int id)
    {
        var category = await _context.ProductCategories!.FindAsync(id);
        return category;
    }

    public async Task<IEnumerable<Product>> GetItemsByCategoryAsync(int categoryId)
    {
        var products = await (from  product in _context.Products!
                              where product.CategoryId == categoryId
                              select product).ToListAsync();

        return products;
    }
}