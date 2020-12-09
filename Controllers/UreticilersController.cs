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
    public class UreticilersController : ControllerBase
    {
        private readonly UreticilerDbContext _context;

        public UreticilersController(UreticilerDbContext context)
        {
            _context = context;
        }

        // GET: api/Ureticilers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ureticiler>>> GetUretici()
        {
            return await _context.Ureticiler.ToListAsync();
        }

        // GET: api/Ureticilers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ureticiler>> GetUreticiler(int id)
        {
            var ureticiler = await _context.Ureticiler.FindAsync(id);

            if (ureticiler == null)
            {
                return NotFound();
            }

            return ureticiler;
        }

        // PUT: api/Ureticilers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUreticiler(int id, Ureticiler ureticiler)
        {
            if (id != ureticiler.ID)
            {
                return BadRequest();
            }

            _context.Entry(ureticiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UreticilerExists(id))
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

        // POST: api/Ureticilers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Ureticiler>> PostUreticiler(Ureticiler ureticiler)
        {
            _context.Ureticiler.Add(ureticiler);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUreticiler", new { id = ureticiler.ID }, ureticiler);
        }

        // DELETE: api/Ureticilers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ureticiler>> DeleteUreticiler(int id)
        {
            var ureticiler = await _context.Ureticiler.FindAsync(id);
            if (ureticiler == null)
            {
                return NotFound();
            }

            _context.Ureticiler.Remove(ureticiler);
            await _context.SaveChangesAsync();

            return ureticiler;
        }

        private bool UreticilerExists(int id)
        {
            return _context.Ureticiler.Any(e => e.ID == id);
        }
    }
}
