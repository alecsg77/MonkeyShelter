using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

using MonkeyShelter.App;
using MonkeyShelter.App.Model;
using MonkeyShelter.Data.Model;
using MonkeyShelter.Web.Model;

namespace MonkeyShelter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonkeysController : ControllerBase
    {
        private readonly IShelterApp _shelter;
        private readonly IMapper _mapper;

        public MonkeysController(IShelterApp shelter, IMapper mapper)
        {
            _shelter = shelter;
            _mapper = mapper;
        }

        // GET: api/Monkeys
        [HttpGet]
        public async Task<ActionResult<MonkeyIndexDto>> GetMonkeys()
        {
            return _mapper.Map<MonkeyRegistry, MonkeyIndexDto>(await _shelter.GetRegistryAsync());
        }

        // GET: api/Monkeys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MonkeyDto>> GetMonkey(string id)
        {
            try
            {
                return _mapper.Map<MonkeyDetails, MonkeyDto>(await _shelter.GetMonkeyDetailsAsync(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/Monkeys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMonkey(string id, PutMonkeyDto monkey)
        {
            if (id != monkey.Id)
            {
                return BadRequest();
            }

            try
            {
                await _shelter.UpdateRegistryAsync(_mapper.Map<PutMonkeyDto, UpdateRegistryRequest>(monkey));
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
        public async Task<ActionResult<PostMonkeyDto>> PostMonkey(PostMonkeyDto monkey)
        {
            try
            {
                await _shelter.RegisterMonkeyAsync(_mapper.Map<PostMonkeyDto, RegisterMonkeyRequest>(monkey));
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
