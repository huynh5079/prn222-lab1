using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using ShopeeFoodChap2.Services.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace ShopeeFoodChap2.Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _httpClient = new HttpClient();
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            _logger.LogInformation("Fetching all orders from the database.");
            var orders = await _orderRepository.GetAllAsync();
            _logger.LogInformation("Fetched {Count} orders from the database.", orders.Count());
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            _logger.LogInformation("Fetching order with ID {Id} from the database.", id);
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                _logger.LogWarning("Order with ID {Id} not found.", id);
            }
            else
            {
                _logger.LogInformation("Fetched order with ID {Id} from the database.", id);
            }
            return order;
        }

        public async Task AddOrderAsync(Order order)
        {
            _logger.LogInformation("Adding a new order to the database.");
            await _orderRepository.AddAsync(order);
            _logger.LogInformation("Added a new order with ID {Id} to the database.", order.Id);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _logger.LogInformation("Updating order with ID {Id} in the database.", order.Id);
            await _orderRepository.UpdateAsync(order);
            _logger.LogInformation("Updated order with ID {Id} in the database.", order.Id);
        }

        public async Task DeleteOrderAsync(int id)
        {
            _logger.LogInformation("Deleting order with ID {Id} from the database.", id);
            await _orderRepository.DeleteAsync(id);
            _logger.LogInformation("Deleted order with ID {Id} from the database.", id);
        }

        public async Task<IEnumerable<Order>> FetchOrdersFromApiAsync()
        {
            try
            {
                _logger.LogInformation("Fetching orders from external API.");
                var response = await _httpClient.GetAsync("http://localhost:5149/api/orders");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(content);
                _logger.LogInformation("Fetched {Count} orders from external API.", orders.Count());
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching orders from external API.");
                return null;
            }
        }

        public Task CheckOrderStatusAsync()
        {
            throw new NotImplementedException();
        }
    }
}
