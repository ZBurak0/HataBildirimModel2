using System.Collections.Generic;
using HataBildirimModel2.Models;

namespace HataBildirimModel2.Repositories
{
    public interface FaultInterface
    {
        Task<IEnumerable<FaultNotification>> GetAllAsync();
        Task<FaultNotification> GetByIdAsync(int id);
        Task CreateAsync(FaultNotification faultNotification);
        Task UpdateAsync(FaultNotification faultNotification);
        Task DeleteAsync(int id);
    }
}
