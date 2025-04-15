using HataBildirimModel.Models;
using HataBildirimModel2.Models;
using Microsoft.EntityFrameworkCore;

namespace HataBildirimModel2.Repositories
{
    public class FaultRepository : FaultInterface
    {
        private readonly AppDbContext _context;

        public FaultRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<FaultNotification>> GetAllAsync()
        {
            return await _context.FaultNotifications
                .Include(f => f.User)
                .Include(f => f.FaulType)
                .Include(f => f.File)
                .ToListAsync();
        }

        public async Task<FaultNotification> GetByIdAsync(int id)
        {
            return await _context.FaultNotifications
                .Include(f => f.User)
                .Include(f => f.FaulType)
                .Include(f => f.File)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task CreateAsync(FaultNotification faultNotification)
        {
            await _context.FaultNotifications.AddAsync(faultNotification);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FaultNotification faultNotification)
        {
            _context.FaultNotifications.Update(faultNotification);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var faultNotification = await GetByIdAsync(id);
            if (faultNotification != null)
            {
                _context.FaultNotifications.Remove(faultNotification);
                await _context.SaveChangesAsync();
            }
        }
    }
}
