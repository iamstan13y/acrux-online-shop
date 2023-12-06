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
        var products = await _context
            .Products!
            .Include(p => p.ProductCategory)
            .ToListAsync();
        
        return products;
    }

    public async Task<Product> GetAsync(int id)
    {
        var product = await _context
            .Products!
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.Id == id);

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
        var products = await _context
            .Products!
            .Include(p => p.ProductCategory)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();

        return products;
    }
}