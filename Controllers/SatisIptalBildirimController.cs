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
    public class SatisIptalBildirimController : ControllerBase
    {
        private readonly SatislarDbContext _SatislarDb;
        
        public SatisIptalBildirimController(SatislarDbContext db1)
        {
            _SatislarDb = db1;
            
        }


        // POST api/<SatisIptalBildirimController>
        [HttpPost]
        public async Task<ActionResult<Satislar>> PostSatisIptalBildirim(Satislar satislar)
        {
            //urunun satiş bilgilerini bul
            Satislar urun = _SatislarDb.Satislar.Where(c => c.GTIN == satislar.GTIN && c.Gonderici_GLN == satislar.Gonderici_GLN).FirstOrDefault();
            if (urun != null)
            {
                urun.Durum = 0;
                await _SatislarDb.SaveChangesAsync();
                return Content("Satış iptal edildi");
                
            }

            else return BadRequest("Ürun satışı bulunamadı");

                   

        }

       
    }
}
