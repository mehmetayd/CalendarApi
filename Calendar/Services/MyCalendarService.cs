using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Calendar.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;

namespace Calendar.Services
{
    public class MyCalendarService
    {
        private string jsonFile = "ezoteric-stream-318615-91d29934ede4.json";
        private string calendarId = @"mehmet.ayd.3411@gmail.com";
        private string CalendarId()
        {
            return @"tr.turkish#holiday@group.v.calendar.google.com";
        }

        private string[] Scopes = { CalendarService.Scope.Calendar };

        private ServiceAccountCredential credential;

        private CalendarService service;

        public MyCalendarService()
        {
            bool initResult = init();
            
            if(initResult == false)
            {
                Console.WriteLine("MyCalendarService initialize failed!");
            }
        }

        public bool init()
        {
            using (var stream =
                new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
            {
                var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(confg.ClientEmail)
                   {
                       Scopes = Scopes
                   }.FromPrivateKey(confg.PrivateKey));
            }

            service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API Sample",
            });

            return true;
        }

        public EventsResource.ListRequest ToplantiListele()
        {
            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            listRequest.TimeMin = new DateTime(2021, 1, 1, 0, 0, 0);
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            return listRequest;
        }

        public bool ToplantiOlustur(Toplanti toplanti)
        {
            try
            {
                Event newEvent = new Event()
                {
                    Summary = toplanti.toplantiAdi,
                    //Location = "800 Howard St., San Francisco, CA 94103",
                    Description = toplanti.aciklama,
                    Start = new EventDateTime()
                    {
                        DateTime = toplanti.baslangicTarihi,
                        TimeZone = "GMT+3:00",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = toplanti.bitisTarihi,
                        TimeZone = "GMT+3:00",
                    },
                    //Recurrence = new String[] { "RRULE:FREQ=DAILY;COUNT=2" },
                    //    Attendees = new EventAttendee[] {
                    //    new EventAttendee() { Email = "lpage@example.com" },
                    //    new EventAttendee() { Email = "sbrin@example.com" },
                    //},
                    //Reminders = new Event.RemindersData()
                    //{
                    //    UseDefault = false,
                    ////    Overrides = new EventReminder[] {
                    ////new EventReminder() { Method = "email", Minutes = 24 * 60 },
                    ////new EventReminder() { Method = "sms", Minutes = 10 },
                    ////}
                    //}
                };

                Event recurringEvent = service.Events.Insert(newEvent, calendarId).Execute();

            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

            return true;
        }
    
        public Events ToplantilariGetir()
        {
            EventsResource.ListRequest listRequest = service.Events.List(calendarId);
            //listRequest.TimeMin = new DateTime(2021, 1, 1, 0, 0, 0);
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            //listRequest.MaxResults = 10;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = listRequest.Execute();

            return events;
        }
        // TatilGunleriniGetir yaptım!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public Events TatilGunleriniGetir(DateTime queryStartDate, DateTime queryEndDate)
        {
            EventsResource.ListRequest listRequest = service.Events.List(CalendarId());
            listRequest.TimeMin = new DateTime(2021, 1, 1, 0, 0, 0);
            //listRequest.TimeMin = queryStartDate;
            //listRequest.TimeMax = queryEndDate;
            listRequest.ShowDeleted = false;
            listRequest.SingleEvents = true;
            listRequest.MaxResults = 50;
            listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            //belirlenen tarih aralıklarındaki tatil günleri getirilecek.!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            // List events.
            Events events = listRequest.Execute();

            return events;
        }

        //public bişeListesi ResmiTatilGunleriniGetir
        // getirip göstericen
    }
}
