using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Repositories;

namespace Resturant_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishRepository _repository;

        public DishController(IDishRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dish = await _repository.GetByIdAsync(id);
            return dish == null ? NotFound() : Ok(dish);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dish dish)
        {
            await _repository.AddAsync(dish);
            return CreatedAtAction(nameof(GetById), new { id = dish.Id }, dish);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Dish dish)
        {
            if (id != dish.Id) return BadRequest();
            await _repository.UpdateAsync(dish);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
