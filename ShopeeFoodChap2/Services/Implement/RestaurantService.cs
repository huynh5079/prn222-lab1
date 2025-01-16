using ShopeeFoodChap2.Models;
using ShopeeFoodChap2.Repositories.Interfaces;
using ShopeeFoodChap2.Services.Interfaces;

namespace ShopeeFoodChap2.Services.Implement
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            return await _restaurantRepository.GetByIdAsync(id);
        }

        public async Task AddRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantRepository.AddAsync(restaurant);
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            await _restaurantRepository.UpdateAsync(restaurant);
        }

        public async Task DeleteRestaurantAsync(int id)
        {
            await _restaurantRepository.DeleteAsync(id);
        }

        public async Task ProcessRestaurantsAsync()
        {
            var restaurants = await _restaurantRepository.GetAllAsync();
            var parallelQuery = restaurants.AsParallel()
                                           .WithDegreeOfParallelism(50) // Setting volume
                                           .Select(r =>
                                           {
                                               UpdateMenu(r);
                                               UpdateStatus(r);
                                               return r;
                                           }).ToList();
        }
        private void UpdateMenu(Restaurant restaurant)
        {
            // Logic update
            restaurant.Menu = "Updated Menu part 3";
            _restaurantRepository.UpdateAsync(restaurant);
        }

        private void UpdateStatus(Restaurant restaurant)
        {
            // Logic update status
            restaurant.IsActive = true;
            _restaurantRepository.UpdateAsync(restaurant);
        }

    }
}
