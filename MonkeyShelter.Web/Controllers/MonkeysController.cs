using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MonkeyShelter.Data;
using MonkeyShelter.Data.Model;

namespace MonkeyShelter.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonkeysController : ControllerBase
    {
        private readonly IMonkeyRepository _repository;

        public MonkeysController(IMonkeyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Monkeys
        [HttpGet]
        public async Task<IReadOnlyCollection<Monkey>> GetMonkeys()
        {
            return await _repository.GetAllAsync();
        }

        // GET: api/Monkeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monkey>> GetMonkey(string id)
        {
            var monkey = await _repository.GetByIdAsync(id);
            if (monkey == null)
            {
                return NotFound();
            }

            return monkey;
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

            if (!await _repository.UpdateAsync(monkey))
                return NotFound();

            return NoContent();
        }

        // POST: api/Monkeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monkey>> PostMonkey(Monkey monkey)
        {
            if (!await _repository.AddAsync(monkey))
                return Conflict();

            return CreatedAtAction("GetMonkeys", new { id = monkey.Id }, monkey);
        }

        // DELETE: api/Monkeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonkey(string id)
        {
            if (!await _repository.RemoveByIdAsync(id))
                return NotFound();

            return NoContent();
        }
    }
}
