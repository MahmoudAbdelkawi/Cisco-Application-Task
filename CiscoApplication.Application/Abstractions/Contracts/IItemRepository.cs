using CiscoApplication.Application.Specifications.ItemSpecs;
using CiscoApplication.Domain.Entities;

namespace CiscoApplication.Application.Abstractions.Contracts
{
    public interface IItemRepository
    {
        Task<IReadOnlyList<Item>> GetItemsWithSpecAsync(ItemSpec itemSpec);
        Task<Item?> GetItemByIdAsync(Guid id);
        Task<int> GetItemsCountWithSpecAsync(ItemSpec itemSpec);
        Task<Item?> GetItemWithSKU(string itemSKU);
        Task AddItemsRangeAsync(List<Item> items);
        Task AddItemAsync(Item item);
        public Task<int> SaveChangesAsync();
    }
}
