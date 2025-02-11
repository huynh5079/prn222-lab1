using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using ShopeeFoodChap2.Services.Interfaces;

namespace ShopeeFoodChap2.Services.Implement
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            return await _driverRepository.GetAllAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            return await _driverRepository.GetByIdAsync(id);
        }

        public async Task AddDriverAsync(Driver driver)
        {
            await _driverRepository.AddAsync(driver);
        }

        public async Task UpdateDriverAsync(Driver driver)
        {
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteDriverAsync(int id)
        {
            await _driverRepository.DeleteAsync(id);
        }
    }
}
