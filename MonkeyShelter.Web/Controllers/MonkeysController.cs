using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.App;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonkeysController : ControllerBase
    {
        private readonly IShelterApp _shelter;

        public MonkeysController(IShelterApp shelter)
        {
            _shelter = shelter;
        }

        // GET: api/Monkeys
        [HttpGet]
        public async Task<IReadOnlyCollection<Monkey>> GetMonkeys()
        {
            return await _shelter.ListRegistryAsync();
        }

        // GET: api/Monkeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monkey>> GetMonkey(string id)
        {
            try
            {
                return await _shelter.GetMonkeyDetailsAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Monkeys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonkey(string id, Monkey monkey)
        {
            if (id != monkey.Id)
            {
                return BadRequest();
            }

            try
            {
                await _shelter.UpdateRegistryAsync(monkey);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Monkeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monkey>> PostMonkey(Monkey monkey)
        {
            try
            {
                await _shelter.RegisterMonkeyAsync(monkey);
            }
            catch (ConflictException)
            {
                return Conflict();
            }

            return CreatedAtAction("GetMonkeys", new { id = monkey.Id }, monkey);
        }

        // DELETE: api/Monkeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonkey(string id)
        {
            try
            {
                await _shelter.ReleaseMonkeyAsync(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
