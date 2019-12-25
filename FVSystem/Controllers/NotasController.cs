using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FVSystem.Models;
using FVSystem.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FVSystem.Controllers
{
    public class NotasController : Controller
    {

        private EstudiantesRepository estudiantesRepository;
        private NotasRepository notasRepository;

        public NotasController(IConfiguration config, IWebHostEnvironment env)
        {
            estudiantesRepository = new EstudiantesRepository(config, env);
            notasRepository = new NotasRepository(config, env);
        }

        public IActionResult Asignar(string estudianteId, string moduloId)
        {
            var desglose = new NotaEstudiante()
            {
                Desglose = notasRepository.DesgloseNotasPorModulo(moduloId, estudianteId),
                Estudiante = estudiantesRepository.ObtenerEstudiante(estudianteId)
            };

            return View(desglose);

        }

        public ActionResult ActualizarNotas(DesgloseNotas desgloseNotas)
        {

            notasRepository.ActualizarNotasEstudiante(desgloseNotas);

            return Redirect("/Notas");
        }

    }
}