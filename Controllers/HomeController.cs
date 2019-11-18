using System;
using System.Data;
using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;



namespace FVSystem.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        private SedesRepository repository;
        public HomeController(IConfiguration config)
        {
            configuration = config;
            repository = new SedesRepository();
        }

        public ActionResult Index()
        {
            var sedes = repository.GetSedes();

            return View(sedes);

        }
    }
}