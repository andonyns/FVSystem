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
        public ActionResult Index()
        {        
            return View();
        }
    }
}