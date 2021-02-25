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
        private readonly ShelterContext _context;

        public MonkeysController(ShelterContext context)
        {
            _context = context;
        }

        // GET: api/Monkeys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Monkey>>> GetMonkey()
        {
            return await _context.Monkey.ToListAsync();
        }

        // GET: api/Monkeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Monkey>> GetMonkey(string id)
        {
            var monkey = await _context.Monkey.FindAsync(id);

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

            _context.Entry(monkey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonkeyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Monkeys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Monkey>> PostMonkey(Monkey monkey)
        {
            _context.Monkey.Add(monkey);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MonkeyExists(monkey.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMonkey", new { id = monkey.Id }, monkey);
        }

        // DELETE: api/Monkeys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonkey(string id)
        {
            var monkey = await _context.Monkey.FindAsync(id);
            if (monkey == null)
            {
                return NotFound();
            }

            _context.Monkey.Remove(monkey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MonkeyExists(string id)
        {
            return _context.Monkey.Any(e => e.Id == id);
        }
    }
}
