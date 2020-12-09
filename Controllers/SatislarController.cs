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
    public class SatislarController : ControllerBase
    {
        private readonly SatislarDbContext _context;

        public SatislarController(SatislarDbContext context)
        {
            _context = context;
        }

        // GET: api/Satislar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Satislar>>> GetSatislar()
        {
            return await _context.Satislar.ToListAsync();
        }

        // GET: api/Satislar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Satislar>> GetSatislar(int id)
        {
            var satislar = await _context.Satislar.FindAsync(id);

            if (satislar == null)
            {
                return NotFound();
            }

            return satislar;
        }

        // PUT: api/Satislar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSatislar(int id, Satislar satislar)
        {
            if (id != satislar.ID)
            {
                return BadRequest();
            }

            _context.Entry(satislar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SatislarExists(id))
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

        // POST: api/Satislar
        
        [HttpPost]
        public async Task<ActionResult<Satislar>> PostSatislar(Satislar satislar)
        {
            
            _context.Satislar.Add(satislar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSatislar", new { id = satislar.ID }, satislar);
        }

        // DELETE: api/Satislar/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Satislar>> DeleteSatislar(int id)
        {
            var satislar = await _context.Satislar.FindAsync(id);
            if (satislar == null)
            {
                return NotFound();
            }

            _context.Satislar.Remove(satislar);
            await _context.SaveChangesAsync();

            return satislar;
        }

        private bool SatislarExists(int id)
        {
            return _context.Satislar.Any(e => e.ID == id);
        }
    }
}
