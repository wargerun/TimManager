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
            return _context.PassItems.SingleOrDefaultAsync(p => p.UserId == userId && p.Name == name);
        }

        public Task<PassItem> GetPassItemAsync(int id)
        {
            return _context.PassItems.FindAsync(id).AsTask();
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
            PassItem passItemDb = await GetPassItemAsync(newPassItem.Id);
            
            if (passItemDb is null)
            {
                throw new InvalidOperationException($"Pass Item не найлен");
            }

            passItemDb.Name = newPassItem.Name;
            passItemDb.Uri = newPassItem.Uri;
            passItemDb.UserName = newPassItem.UserName;
            passItemDb.Description = newPassItem.Description;

            if (!string.IsNullOrWhiteSpace(newPassItem.Password))
            {
                passItemDb.Password = newPassItem.Password;
            }

            _context.PassItems.Update(passItemDb);

            await _context.SaveChangesAsync();
        }

        private Task<bool> PassItemExistAsync(string userId, string name) => _context.PassItems.AnyAsync(e => e.UserId == userId && e.Name == name);

        private Task<bool> PassItemExistAsync(int id) => _context.PassItems.AnyAsync(e => e.Id == id);
    }
}
