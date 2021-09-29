using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Models
{
    public class Person
    {
        public Person(string name, string surname, DateTime birthDay)
        {
            Name = name;
            Surname = surname;
            BirthDay = birthDay;
        }
        
        public string Name { get; }
        public string Surname { get; }
        public DateTime BirthDay { get; }
        public int Age => (int)(DateTime.Now.Date - BirthDay.Date).TotalDays / 365;
    }
}
