using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using ShopeeFoodChap2.Services.Interfaces;
using Newtonsoft.Json;

namespace ShopeeFoodChap2.Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly HttpClient _httpClient;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
        //Add
        public async Task<IEnumerable<Order>> FetchOrdersFromApiAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5149/api/orders"); 
                response.EnsureSuccessStatusCode();
                // excute response
                var content = await response.Content.ReadAsStringAsync(); 
                var orders = JsonConvert.DeserializeObject<IEnumerable<Order>>(content); 
                return orders; 
            } 
            catch (Exception ex) 
            { // error catch
                Console.WriteLine($"Error fetching orders: {ex.Message}"); 
                return null; 
            }
        }

        public Task CheckOrderStatusAsync()
        {
            throw new NotImplementedException();
        }
    }
}
