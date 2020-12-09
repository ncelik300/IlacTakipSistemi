using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webApiDeneme.Models;

namespace webApiDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunlerController : ControllerBase
    {
        private readonly UrunlerDbContext _context;

        public UrunlerController(UrunlerDbContext context)
        {
            _context = context;
        }

        // GET: api/Urunler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Urunler>>> GetUrunler()
        {
            return await _context.Urunler.ToListAsync();
        }

        // GET: api/Urunler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Urunler>> GetUrunler(int id)
        {
            var urunler = await _context.Urunler.FindAsync(id);

            if (urunler == null)
            {
                return NotFound();
            }

            return urunler;
        }

        // PUT: api/Urunler/5
    
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUrunler(int id, Urunler urunler)
        {
            if (id != urunler.ID)
            {
                return BadRequest();
            }

            _context.Entry(urunler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrunlerExists(id))
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

        // POST: api/Urunler
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Urunler>> PostUrunler(Urunler urunler)
        {
            _context.Urunler.Add(urunler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUrunler", new { id = urunler.ID }, urunler);
        }

        // DELETE: api/Urunler/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Urunler>> DeleteUrunler(int id)
        {
            var urunler = await _context.Urunler.FindAsync(id);
            if (urunler == null)
            {
                return NotFound();
            }

            _context.Urunler.Remove(urunler);
            await _context.SaveChangesAsync();

            return urunler;
        }

        private bool UrunlerExists(int id)
        {
            return _context.Urunler.Any(e => e.ID == id);
        }
    }
}
