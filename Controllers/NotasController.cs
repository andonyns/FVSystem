using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FVSystem.Controllers
{
    public class NotasController : Controller
    {
        public IActionResult Asignar()
        {
            return View();
        }
    }
}