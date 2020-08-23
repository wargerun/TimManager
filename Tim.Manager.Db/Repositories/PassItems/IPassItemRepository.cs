using System.Linq;
using System.Threading.Tasks;
using Tim.Manager.Db.Entities;

namespace Tim.Manager.Db.Repositories.PassItems
{
    public interface IPassItemRepository
    {
        IQueryable<PassItem> GetPassItems(string userId);

        Task InsertAsync(PassItem newPassItem);

        Task<PassItem> GetPassItemAsync(string userId, string name);
        Task<PassItem> GetPassItemAsync(int id);

        Task UpdateAsync(PassItem newPassItem);
        Task DeleteAsync(int id);
    }
}
