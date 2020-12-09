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
    public class MalAlimBildirimController : ControllerBase
    {

        private readonly SatislarDbContext _SatislarDb;
        private readonly StoklarDbContext _StoklarDb;

        public MalAlimBildirimController(SatislarDbContext db1, StoklarDbContext db2)
        {
            _SatislarDb = db1;
            _StoklarDb = db2;
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
        // POST api/<MalAlimBildirimController>
        [HttpPost]
        public async Task<ActionResult<Satislar>> PostMalAlimBildirim(Satislar malAlim)
        {
            //urunün alıcısı ile mal bidirimi yapan aynı ise ürünü alıcının stoklarına aktar
            Satislar urun = _SatislarDb.Satislar.Where(c => c.GTIN == malAlim.GTIN && c.Alici_GLN== malAlim.Alici_GLN).FirstOrDefault();
            Stoklar oncekiSahip = _StoklarDb.Stoklar.Where(k => k.GTIN == malAlim.GTIN).FirstOrDefault();
            if (urun != null)
            {
                Stoklar _stok = new Stoklar();
                _stok.GLN = urun.Alici_GLN;
                _stok.GTIN = urun.GTIN;
                _stok.UrunID = oncekiSahip.UrunID;
                _stok.Durum = 1;
                _StoklarDb.Stoklar.Add(_stok);
                oncekiSahip.Durum = 0; //ürün satının stoğunda pasif yapıldı
                urun.Durum = 1; //Satıcının satışı onaylandı
                await _StoklarDb.SaveChangesAsync();
                await _SatislarDb.SaveChangesAsync();
                return CreatedAtAction("GetStokGetir", _stok);
            }

            else return BadRequest("Ürun envanterde yok");

                    

        }

        // PUT api/<MalAlimBildirimController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMalAlimBildirim(int id, Satislar satislar)
        {
            if (id != satislar.ID)
            {
                return BadRequest();
            }

            _SatislarDb.Entry(satislar).State = EntityState.Modified;

            try
            {
                await _SatislarDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MalAlimBildirimExists(id))
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

        private bool MalAlimBildirimExists(int id)
        {
            return _SatislarDb.Satislar.Any(e => e.ID == id);
        }
        // DELETE api/<MalAlimBildirimController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
