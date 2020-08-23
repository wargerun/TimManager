using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tim.Manager.Db.Data;
using Tim.Manager.Db.Entities;

namespace Tim.Manager.Db.Repositories.PassItems
{
    public class PassItemRepository : IPassItemRepository
    {
        public static readonly string ItemPropertyKey = "ItemProperty";
        private readonly ManagerDbContext _context;

        public PassItemRepository(ManagerDbContext managerDbContext)
        {
            _context = managerDbContext;
        }

        public Task<PassItem> GetPassItemAsync(string userId, string name)
        {
            return _context.PassItems.FindAsync(userId, name).AsTask();
        }

        public IQueryable<PassItem> GetPassItems(string userId) => getPassItemsInternal(userId);

        public IQueryable<PassItem> getPassItemsInternal(string userId)
        {
            return _context.PassItems.OrderByDescending(p => p.Created).Where(p => p.UserId == userId).AsQueryable();
        }

        public async Task InsertAsync(PassItem newPassItem)
        {
            await throwPassItemIfNotExistAsync(newPassItem.UserId, newPassItem.Name);

            _context.PassItems.Add(newPassItem);
            await _context.SaveChangesAsync();
        }

        private async Task throwPassItemIfNotExistAsync(string userId, string name)
        {
            if (await PassItemExistAsync(userId, name))
            {
                var ex = new InvalidOperationException($"Pass Item с таким названием уже существует");
                ex.Data[ItemPropertyKey] = nameof(name);
                throw ex;
            }
        }

        public async Task UpdateAsync(PassItem newPassItem)
        {
            await throwPassItemIfNotExistAsync(newPassItem.UserId, newPassItem.Name);

            PassItem passItemDb = await GetPassItemAsync(newPassItem.UserId, newPassItem.Name);
            newPassItem.CopyTo(passItemDb);
            _context.PassItems.Update(passItemDb);

            await _context.SaveChangesAsync();
        }

        public Task<bool> PassItemExistAsync(string userId, string name) => _context.PassItems.AnyAsync(e => e.UserId == userId && e.Name == name);
    }
}
