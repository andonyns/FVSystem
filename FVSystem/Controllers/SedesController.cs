using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Controllers
{
    public class SedesController : Controller
    {
        private SedesRepository repository;

        public SedesController(IConfiguration config)
        {
            repository = new SedesRepository(config);
        }
        public IActionResult Index()
        {
            var sedes = repository.GetSedes();

            return View(sedes);
        }
    }
}
