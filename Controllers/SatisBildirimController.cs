using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApiDeneme.Models;


namespace webApiDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisBildirimController : ControllerBase
    {
        private readonly SatislarDbContext _SatislarDb;
        

        public SatisBildirimController(SatislarDbContext db1)
        {
            _SatislarDb = db1;
            
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Satislar>>> GetSatis()
        {
            return await _SatislarDb.Satislar.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Satislar>> GetSatislar(int id)
        {
            var satislar = await _SatislarDb.Satislar.FindAsync (id);

            if (satislar == null)
            {
                return NotFound();
            }

            return satislar;
        }

       
        // POST api/<SatisBildirimController>
        [HttpPost]
        public async Task<ActionResult<Satislar>> PostSatisBildirim(Satislar satislar)
        {
            //urun daha önce satılıp satılmadığını doğrula
            Satislar urun = _SatislarDb.Satislar.Where(c => c.GTIN == satislar.GTIN && c.Gonderici_GLN==satislar.Gonderici_GLN).FirstOrDefault();
            if (urun == null)
            {
                _SatislarDb.Satislar.Add(satislar);
                await _SatislarDb.SaveChangesAsync();
                return CreatedAtAction("GetSatislar", new { id = satislar.ID }, satislar);
            }

            else return BadRequest("Ürun satışı daha önce yapıldı");
            
            //bu aşamada ürün henüz satıcının stoklarında, alıcının mal alım bildirimine kadar.          
            
        }

        
        

        
    }
}
