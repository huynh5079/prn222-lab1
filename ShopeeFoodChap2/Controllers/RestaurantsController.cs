using Microsoft.AspNetCore.Mvc;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRestaurants()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantById(int id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
    {
        await _restaurantService.AddRestaurantAsync(restaurant);
        return CreatedAtAction(nameof(GetRestaurantById), new { id = restaurant.Id }, restaurant);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRestaurant(int id, Restaurant restaurant)
    {
        if (id != restaurant.Id)
        {
            return BadRequest();
        }

        await _restaurantService.UpdateRestaurantAsync(restaurant);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRestaurant(int id)
    {
        await _restaurantService.DeleteRestaurantAsync(id);
        return NoContent();
    }
}
