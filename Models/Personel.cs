using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoreProject.Models
{
    public class Personel
    {
        [Key]

        public int PersonelID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Sehir { get; set; }
        public int BirimID { get; set; }
        public Birim Birim { get; set; }
    }
}
