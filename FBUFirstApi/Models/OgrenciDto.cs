using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBUFirstApi.Models
{
    public class OgrenciDto: BaseDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }

        public string Soyad { get; set; }
        public string Tel { get; set; }

    }
}
