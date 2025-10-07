using checkpoint__10072025.Data;
using checkpoint__10072025.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace checkpoint__10072025.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly AppDbContext _db;
        public ClientesController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
    => View(await _db.Clientes.AsNoTracking().ToListAsync());


        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(Cliente model)
        {
            if (!ModelState.IsValid) return View(model);
            _db.Clientes.Add(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var c = await _db.Clientes.FindAsync(id);
            return c == null ? NotFound() : View(c);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(long id, Cliente model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);
            _db.Update(model);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var c = await _db.Clientes.FindAsync(id);
            return c == null ? NotFound() : View(c);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var c = await _db.Clientes.FindAsync(id);
            if (c != null) { _db.Clientes.Remove(c); await _db.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
