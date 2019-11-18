using FVSystem.Models;
using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace FVSystem.Controllers
{
    public class EstudiantesController : Controller
    {
        private EstudiantesRepository repository;

        public EstudiantesController(IConfiguration config)
        {
            repository = new EstudiantesRepository(config);
        }

        public ActionResult Index()
        {
            var estudiantes = repository.ObtenerEstudiantes();

            return View("Lista", estudiantes);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Editar(string id)
        {
            var estudiante = repository.ObtenerEstudiante(id);

            return View(estudiante);
        }

        public ActionResult Guardar(Estudiante newEstudiante)
        {
            repository.InsertarEstudiante(newEstudiante);

            return Redirect("/Estudiantes");
        }

        public ActionResult Actualizar(Estudiante estudiante)
        {

            repository.ActualizarEstudiante(estudiante);

            return Redirect("/Estudiantes");
        }

        [HttpPost]
        public ActionResult Eliminar(string id)
        {   
            var estudiante = repository.BorrarEstudiante(id);

            return Ok();
        }

        public ActionResult EstudiantesPorCurso(string id)
        {

            var estudiantes = repository.ObtenerEstudiantesCurso(id);
			if (estudiantes == null || estudiantes.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron estudiantes";
            }

            return View("Lista", estudiantes);
        }
    }
}
