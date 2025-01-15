using ShopeeFoodChap2.Models;

namespace ShopeeFoodChap2.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

        Task<IEnumerable<Order>> FetchOrdersFromApiAsync();
    }
}
