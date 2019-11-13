using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FVSystem.Controllers
{
    public class HomeController : Controller
    {
        private SedesRepository repository;
        public HomeController()
        {
            repository = new SedesRepository();
        }
        // GET: Home
        public ActionResult Index()
        {
            var sedes = repository.GetSedes();

            return View(sedes);
        
        }



    }
}