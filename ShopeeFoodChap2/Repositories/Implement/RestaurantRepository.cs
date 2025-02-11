using ShopeeFoodChap2.Data;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShopeeFoodChap2.Repositories.Implement
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ShopeeFoodChap2Context _context;

        public RestaurantRepository(ShopeeFoodChap2Context context)
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

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
