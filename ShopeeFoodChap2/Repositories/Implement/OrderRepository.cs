using ShopeeFoodChap2.Data;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ShopeeFoodChap2.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopeeFoodChap2Context _context;

        public OrderRepository(ShopeeFoodChap2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
