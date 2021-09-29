using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Toplanti
    {
        public Toplanti(string toplantiAdi, string aciklama, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            this.toplantiAdi = toplantiAdi;
            this.aciklama = aciklama;
            this.baslangicTarihi = baslangicTarihi;
            this.bitisTarihi = bitisTarihi;
        }

        public string toplantiAdi{ get; set; }
        public string aciklama{ get; set; }
        public DateTime baslangicTarihi { get; set; }
        public DateTime bitisTarihi { get; set; }
        //private Person olusturan;

        //public override string ToString()
        //{
        //    return toplantiAdi + " " + aciklama + " " + baslangicTarihi.ToString() + " - " + bitisTarihi.ToString();
        //}
    }
}
