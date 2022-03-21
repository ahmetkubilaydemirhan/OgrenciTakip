using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Ogretmen:BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }

        public virtual List<Ogrenci> Ogrenciler { get; set; }
    }


}
