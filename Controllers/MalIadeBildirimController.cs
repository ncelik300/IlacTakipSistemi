using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApiDeneme.Models;
using System.Collections;


namespace webApiDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MalIadeBildirimController : ControllerBase
    {
        
        private readonly StoklarDbContext _StoklarDb;
        private readonly SatislarDbContext _SatislarDb;

        public MalIadeBildirimController( StoklarDbContext db1, SatislarDbContext db2)
        {
           
            _StoklarDb = db1;
            _SatislarDb = db2;
        }

        public async Task<ActionResult<Stoklar>> GetStokGetir(Stoklar stok)
        {
            var urun = _StoklarDb.Stoklar.Where(c => c.GTIN == stok.GTIN).FirstOrDefault();

            if (urun == null)
            {
                return NotFound();
            }

            return urun;
        }
        // POST api/<MalIadeBildirimController>
        [HttpPost]
        public async Task<ActionResult<Stoklar>> PostMalIadeBildirim(Stoklar malIade)
        {
            //ürünün satıcısını bul
            Satislar OncekiSahip = _SatislarDb.Satislar.Where(z => z.GTIN == malIade.GTIN && z.Alici_GLN == malIade.GLN).FirstOrDefault();
            Stoklar Sahip = _StoklarDb.Stoklar.Where(k => k.GTIN == malIade.GTIN).FirstOrDefault();
            
            if (Sahip != null && Sahip.Durum==0) 
            {
                 Sahip.GLN = OncekiSahip.Gonderici_GLN;                
                 Sahip.Durum = 1;        
                 await _StoklarDb.SaveChangesAsync();                
                 return CreatedAtAction("GetStokGetir", Sahip);
            }

            else return BadRequest("Bu ürün stoklarınızda aktif değil");



        }
    }
}
