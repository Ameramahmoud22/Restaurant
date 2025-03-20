using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Repositories;

namespace Resturant_Project.Controllers
{
    
    
        [Route("api/[controller]")]
        [ApiController]
        public class RestaurantController : ControllerBase
        {
            private readonly RestaurantRepository _repository;

            public RestaurantController(RestaurantRepository repository)
            {
                _repository = repository;
            }

            // GET: api/restaurant
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var restaurants = await _repository.GetAllAsync();
                return Ok(restaurants);
            }

            // GET: api/restaurant/{id}
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var restaurant = await _repository.GetByIdAsync(id);
                if (restaurant == null)
                    return NotFound();

                return Ok(restaurant);
            }

            // POST: api/restaurant
            [HttpPost]
            public async Task<IActionResult> Create([FromBody] Restaurant restaurant)
            {
                if (restaurant == null)
                    return BadRequest();

                await _repository.AddAsync(restaurant);
                return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
            }

            // PUT: api/restaurant/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] Restaurant updatedRestaurant)
            {
                if (id != updatedRestaurant.Id)
                    return BadRequest();

                await _repository.UpdateAsync(updatedRestaurant);
                return NoContent();
            }

            // DELETE: api/restaurant/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
        }
}
