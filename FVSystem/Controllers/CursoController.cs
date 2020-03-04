using FVSystem.Repository;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using FVSystem.ViewModels;

namespace FVSystem.Controllers
{
    public class CursoController : Controller
    {
        private readonly ILogger<CursoController> _logger;
        private CursosRepository repository;

        public CursoController(ILogger<CursoController> logger, IConfiguration config, IWebHostEnvironment env)
        {
            _logger = logger;
            repository = new CursosRepository(config, env);
        }

        public ActionResult Obtener(int programa)
        {
            var cursos = repository.ObtenerCursos(programa);
            if (cursos == null || cursos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron cursos";
            }

            return View("Lista", new CursosPrograma { Cursos = cursos, Programa = programa });
        }

        public ActionResult Agregar(int programa)
        {
            return View(programa);
        }

        public ActionResult Editar(int Id)
        {
            var curso = repository.ObtenerCurso(Id);

            return View(curso);
        }

        [HttpPost]
        public ActionResult Guardar(string nombre, int programa)
        {
            var cursos = repository.InsertarCurso(nombre, programa);

            return Redirect("/Curso");
        }

        public ActionResult Actualizar(Curso curso)
        {
            var cursos = repository.ActualizarCurso(curso);

            return Redirect("/Curso");
        }

        [HttpPost]
        public ActionResult Eliminar(Curso curso)
        {
            var cursos = repository.EliminarCurso(curso);

            return Ok();
        }


    }

}