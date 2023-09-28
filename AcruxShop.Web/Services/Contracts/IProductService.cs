using AcruxShop.Models.Dtos;

namespace AcruxShop.Web.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetAsync(int id);
}