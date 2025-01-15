using ShopeeFoodChap2.Data;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShopeeFoodChap2.Repositories.Implement
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ShopeeFoodDbContext _context;

        public RestaurantRepository(ShopeeFoodDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant> GetByIdAsync(int id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public async Task AddAsync(Restaurant entity)
        {
            await _context.Restaurants.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant entity)
        {
            _context.Restaurants.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Restaurants.FindAsync(id);
            if (entity != null)
            {
                _context.Restaurants.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
