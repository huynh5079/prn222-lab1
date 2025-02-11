using Microsoft.Extensions.Hosting;
using ShopeeFoodChap2.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

public class OrderStatusBackgroundService : BackgroundService
{
    private readonly IOrderService _orderService;

    public OrderStatusBackgroundService(IOrderService orderService)
    {
        _orderService = orderService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Check status
                await _orderService.CheckOrderStatusAsync();

                Console.WriteLine("Order status checked.");
            }
            catch (Exception ex)
            {
                // Handling
                Console.WriteLine($"Error checking order status: {ex.Message}");
            }

            // Tạm dừng 5 giây giữa các lần kiểm tra
            await Task.Delay(5000, stoppingToken);
        }
    }
}
