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

        private EstudiantesRepository estudiantesRepository;
        private NotasRepository notasRepository;

        public NotasController(IConfiguration config)
        {
            estudiantesRepository = new EstudiantesRepository(config);
            notasRepository = new NotasRepository(config);
        }

        public IActionResult Asignar(string id)
        {


            var estudiante = estudiantesRepository.ObtenerEstudiante(id);

            return View(estudiante);

        }

        public ActionResult NotasPorModulo(string moduloId)
        {

            var notas = notasRepository.DesgloseNotasPorModulo(moduloId);
            if (notas == null || notas.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron notas para el modulo: " + moduloId;
            }

            return View("Notas", notas);
        }
    }
}