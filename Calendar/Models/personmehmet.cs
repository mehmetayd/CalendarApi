using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class personmehmet
    {
        public personmehmet (string name, string surname,DateTime workdays)
        {
            Name = name;
            Surname = surname;
            Workdays = workdays;
        }

        public string Name { get; }
        public string Surname { get; }
        public DateTime Workdays { get; }

        //public int Day => (int)(DateTime.Now.Date);

       //bu sayfada 30 çalışma gününün hangi aralıklara geldiğini yapacam    sadece günü sadece
       //başka bir sayfada ise iki tarih arasındaki çalışacağı gün sayısını hesaplayacam
    }
}
