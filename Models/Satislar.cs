using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webApiDeneme.Models
{
    public class Satislar
    {
		public int ID { get; set; }
		public int Gonderici_GLN { get; set; }
		public int Alici_GLN { get; set; }
		public int GTIN { get; set; }
		public string HastaTC { get; set; }
		public int EID { get; set; }
		public int RKN { get; set; }
		public DateTime BildirimTarihi { get; set; }	
		public int Durum { get; set; }

	}
}
