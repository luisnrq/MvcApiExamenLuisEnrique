using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApiExamenLuisEnrique.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Series()
        {
            return View();
        }
    }
}
