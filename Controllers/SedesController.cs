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
        private IConfiguration configuration;
        private SedesRepository repository;

        public SedesController(IConfiguration config)
        {
            configuration = config;
            repository = new SedesRepository();
        }
        public IActionResult Index()
        {
            var sedes = repository.GetSedes();

            return View(sedes);
        }
    }
}
