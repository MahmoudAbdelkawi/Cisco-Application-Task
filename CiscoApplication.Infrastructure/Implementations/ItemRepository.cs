using CiscoApplication.Application.Abstractions.Contracts;
using CiscoApplication.Application.Specifications.ItemSpecs;
using CiscoApplication.Domain.Entities;
using CiscoApplication.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CiscoApplication.Infrastructure.Implementations
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddItemAsync(Item item)
        {
            await _dbContext.Items.AddAsync(item);
        }

        public async Task AddItemsRangeAsync(List<Item> items)
        {
            await _dbContext.Items.AddRangeAsync(items);
        }

        public async Task<Item?> GetItemByIdAsync(Guid id)
        {
            return await _dbContext.Items.FindAsync(id);
        }

        public async Task<int> GetItemsCountWithSpecAsync(ItemSpec itemSpec)
        {
            var start = _dbContext.Items.AsNoTracking().AsQueryable();
            var DataAsQueryable = SpecificationEvaluator<Item>.GetQueryWithSpec(start, itemSpec);
            return await DataAsQueryable.CountAsync();
        }

        public async Task<IReadOnlyList<Item>> GetItemsWithSpecAsync(ItemSpec itemSpec)
        {
            IQueryable<Item>? start = _dbContext.Items.AsNoTracking().AsQueryable();
            var DataAsQueryable = SpecificationEvaluator<Item>.GetQueryWithSpec(start, itemSpec);
            return await DataAsQueryable.ToListAsync();
        }

        public async Task<Item?> GetItemWithSKU(string itemSKU)
        {
            return await _dbContext.Items.AsNoTracking()
                .Where(Item => Item.PartSKU == itemSKU)
                .FirstOrDefaultAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
