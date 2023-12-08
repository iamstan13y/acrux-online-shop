using AcruxShop.Models.Dtos;

namespace AcruxShop.Web.Services.Contracts;

public interface IManageCartItemsLocalStorageService
{
    Task<List<CartItemDto>> GetCollectionAsync();
    Task SaveCollection(List<CartItemDto> cartItemDtos);
    Task RemoveCollection();
} 