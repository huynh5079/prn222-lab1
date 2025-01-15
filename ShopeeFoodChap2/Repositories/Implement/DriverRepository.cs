using Microsoft.EntityFrameworkCore;
using ShopeeFoodChap2.Data;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;

namespace ShopeeFoodChap2.Repositories.Implement
{
    public class DriverRepository : IDriverRepository
    {
        private readonly ShopeeFoodDbContext _context;

        public DriverRepository(ShopeeFoodDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _context.Drivers.ToListAsync();
        }

        public async Task<Driver> GetByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task AddAsync(Driver entity)
        {
            await _context.Drivers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Driver entity)
        {
            _context.Drivers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Drivers.FindAsync(id);
            if (entity != null)
            {
                _context.Drivers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
