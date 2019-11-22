using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FVSystem.Controllers
{
    public class NotasController : Controller
    {
        private EstudiantesRepository repository;

        public NotasController(IConfiguration config)
        {
            repository = new EstudiantesRepository(config);
        }

        public IActionResult Asignar(string id)
        {


            var estudiante = repository.ObtenerEstudiante(id);

            return View(estudiante);

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