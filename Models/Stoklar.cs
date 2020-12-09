using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiDeneme.Models
{
    public class Stoklar
    {
        public int ID { get; set; }
        public int GLN { get; set; }
        public int GTIN { get; set; }
        public int UrunID{ get; set; }
        public int Durum { get; set; }
    }
}
