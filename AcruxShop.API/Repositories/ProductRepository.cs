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

    public Task<Product> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProductCategory>> GetCategoriesAsync()
    {
        var categories = await _context.ProductCategories!.ToListAsync();
        return categories;
    }

    public Task<ProductCategory> GetCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}