using Microsoft.AspNetCore.Mvc;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // FetchOrdersFromApiAsync
    [HttpGet("fetch-external-orders")]
    public async Task<IActionResult> FetchExternalOrders()
    {
        var orders = await _orderService.FetchOrdersFromApiAsync();
        if (orders == null)
        {
            return BadRequest("Error fetching external orders.");
        }
        return Ok(orders);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrder(Order order)
    {
        await _orderService.AddOrderAsync(order);
        return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        await _orderService.UpdateOrderAsync(order);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        await _orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}
