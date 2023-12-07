using AcruxShop.Models.Dtos;

namespace AcruxShop.Web.Services.Contracts;

public interface IManageProductsLocalStorageService
{
    Task<IEnumerable<ProductDto>> GetCollectionAsync();
    Task RemoveCollection();
} 