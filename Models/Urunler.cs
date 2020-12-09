using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiDeneme.Models
{
    public class Urunler
    {
        public int ID { get; set; }
        public int Gonderici { get; set; }
        public int GTIN { get; set; }
        public int UrunTipi { get; set; }
        public DateTime UretimTarihi { get; set; }
        public DateTime BildirimTarihi { get; set; }
        public DateTime SonKullanmaTarihi { get; set; }
        public int PartiNo { get; set; }
        public int Durum { get; set; }
       

    }
}
