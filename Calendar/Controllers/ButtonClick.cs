using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Controllers
{
    public class ButtonClick : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
