using checkpoint__10072025.Data;
using checkpoint__10072025.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace checkpoint__10072025.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ClientesApiController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _db.Clientes.AsNoTracking().ToListAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
            => Ok(await _db.Clientes.FindAsync(id) ?? (object)NotFound());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Cliente c)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _db.Clientes.Add(c); await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = c.Id }, c);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Cliente c)
        {
            if (id != c.Id) return BadRequest();
            _db.Entry(c).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c == null) return NotFound();
            _db.Clientes.Remove(c); await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
