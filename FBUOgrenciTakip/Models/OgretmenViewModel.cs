using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FBUOgrenciTakip.Models
{
    public class OgretmenViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Ad { get; set; }
        [Required]
        public string Soyad { get; set; }
        public List<OgrenciViewModel> AtanmisOgrenciler { get; set; } = new List<OgrenciViewModel>();
        public List<OgrenciViewModel> BosOgrenciler { get; set; } = new List<OgrenciViewModel>();
    }
}
