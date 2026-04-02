using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZooAPI.Data;
using ZooAPI.Models;

namespace ZooAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimalsController : ControllerBase
    {
        private readonly ZooContext _context;

        public AnimalsController(ZooContext context)
        {
            _context = context;
        }

        // GET: api/animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        // POST: api/animals (optional but good)
        [HttpPost]
        public async Task<ActionResult<Animal>> AddAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnimals), new { id = animal.Id }, animal);
        }
    }
}