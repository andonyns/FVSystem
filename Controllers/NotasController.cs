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
    
    public ActionResult NotaPorModulo(int modulo)
    {
            //var notas = repository.ObtenerNotaPorModulo(modulo);
            //if (notas  == null || notas.Count == 0)
            //{
            //    ViewBag.ErrorMessage = "No se encontraron cursos";
            //}

            //return View("Lista", notas);
            return null;
        }
    }
}