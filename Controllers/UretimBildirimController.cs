using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApiDeneme.Models;
using Microsoft.AspNetCore.Authorization;

namespace webApiDeneme.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UretimBildirimController : ControllerBase
    {
        private readonly UrunlerDbContext _UrunlerDb;
        private readonly StoklarDbContext _StoklarDb;

        public UretimBildirimController(UrunlerDbContext db1, StoklarDbContext db2)
        {
            _UrunlerDb = db1;
            _StoklarDb = db2;            
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Urunler>> GetUrunler(int id)
        {
            var urunler = await _UrunlerDb.Urunler.FindAsync(id);

            if (urunler == null)
            {
                return NotFound();
            }

            return urunler;
        }
        // POST api/<UretimBildirimController>
        [HttpPost]
        public async Task<ActionResult<Urunler>> PostUretimBildirim(Urunler urunler)
        {   
            Stoklar _stok = new Stoklar();
            _UrunlerDb.Urunler.Add(urunler);
            await _UrunlerDb.SaveChangesAsync();

            //Üretim bildiriminde ürünler üreticinin stoklarına girer
            _stok.GLN = urunler.Gonderici;
            _stok.GTIN = urunler.GTIN;
            _stok.UrunID = urunler.ID;
            _stok.Durum = 1;
            _StoklarDb.Stoklar.Add(_stok);
            await _StoklarDb.SaveChangesAsync();

            return CreatedAtAction("GetUrunler", new { id = urunler.ID }, urunler);
        }

        // PUT api/<UretimBildirimController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
