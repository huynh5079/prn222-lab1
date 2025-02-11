using Microsoft.AspNetCore.Mvc;
using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class DriversController : ControllerBase
{
    private readonly IDriverService _driverService;

    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        var drivers = await _driverService.GetAllDriversAsync();
        return Ok(drivers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDriverById(int id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }
        return Ok(driver);
    }

    [HttpPost]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        await _driverService.AddDriverAsync(driver);
        return CreatedAtAction(nameof(GetDriverById), new { id = driver.Id }, driver);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver(int id, Driver driver)
    {
        if (id != driver.Id)
        {
            return BadRequest();
        }

        await _driverService.UpdateDriverAsync(driver);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDriver(int id)
    {
        await _driverService.DeleteDriverAsync(id);
        return NoContent();
    }
}
