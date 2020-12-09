using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using webApiDeneme.Models;
using System.Collections;

namespace IlacTakipSistemi.Controllers
{
    public class DeaktivasyonBildirimController : Controller
    {
        
        private readonly StoklarDbContext _StoklarDb;

        public DeaktivasyonBildirimController(StoklarDbContext db2)
        {
            _StoklarDb = db2;
        }

        [HttpPost]
        public async Task<ActionResult<Stoklar>> PostDeaktivasyonBildirim(int gtin)
        {
            //urunün alıcısı ile mal bidirimi yapan aynı ise ürünü alıcının stoklarına aktar
            
            Stoklar urun = _StoklarDb.Stoklar.Where(k => k.GTIN == gtin).FirstOrDefault();
            if (urun != null)
            {
                urun.Durum = 2;
                await _StoklarDb.SaveChangesAsync();                
                return Content("Ürün sistemden çıkarıldı");
            }
           
            else return BadRequest("Ürun envanterde yok");
        }
    }
}
