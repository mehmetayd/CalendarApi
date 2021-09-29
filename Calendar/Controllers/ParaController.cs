using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar;
using Google.Apis.Calendar.v3.Data;
using Calendar.Models;

namespace Calendar.Controllers
{
    public class ParaController : Controller
    {
        private EftModel eft = new EftModel();

        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Index(DateTime tarih)
        {
            if(EFTControl(tarih) == false)
            {
                return View();
            }

            return View(eft);
        }

        public IList<Event> TatilGunuGetir()
        {
            return new List<Event>();
            //return HomeController.myCalendarService.TatilGunleriniGetir().Items;
        }
            /*önce tarihleri getir CalendarService'ten
        parametreyle gelen tatiller içerisinde mi bak
            if içindeyse bir gün ilerlet.(BAŞA DÖN)
            else değilse devam et aşağı

             parametre tarihinin gününü al(DayOfWeek)
              if "Saturday" veya "Sunday" ise bir gün ilerlet(AddDays(1)).(BAŞA DÖN)
              else devam et.

        hiç ilerleme yapmadıysan saate bak, 17:00'dan sonraysa ilerle.(BAŞA DÖN)
        ""	""	""	""	"", değilse EFT BUGÜN !!!!!*/

        public bool EFTControl(DateTime tarih)
        {
            List<Event> tatilGunleri = TatilGunuGetir().ToList();

            List<Event> filtered = tatilGunleri.FindAll(e => e.Start.Date.Equals(tarih.ToString("yyyy-MM-dd")));

            if (filtered.Count != 0 || tarih.Date.DayOfWeek.ToString().Equals("Saturday") || tarih.Date.DayOfWeek.ToString().Equals("Sunday"))
            {
                //tarih = tarih.AddDays(1);
                return EFTControl(tarih.AddDays(1));
            }

            // kod buraya gelmişse gün sorunu kalmamış demektir.

            // saate bakmıyoruz!!!

            Console.WriteLine("EFT gerçekleşme tarihi: " + tarih.Date + " " + tarih.Date.DayOfWeek.ToString());
            //ViewBag.Name = "EFT gerçekleşme tarihi: " + tarih.Date + " " + tarih.Date.DayOfWeek.ToString();

            eft.EftTarih = tarih.Date;

            return true;
        }

    }
}
