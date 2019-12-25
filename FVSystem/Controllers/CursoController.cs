using FVSystem.Repository;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

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

        public ActionResult Index()
        {
            var cursos = repository.ObtenerCursos();

            return View("Lista", cursos);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Editar(int Id)
        {
            var curso = repository.ObtenerCurso(Id);

            return View(curso);
        }

        [HttpPost]
        public ActionResult Guardar(string nombre)
        {
            var cursos = repository.InsertarCurso(nombre);

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

        public ActionResult CursosPorSede(int sede)
        {
            var cursos = repository.ObtenerCursosPorPrograma(sede);
            if (cursos == null || cursos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron cursos";
            }

            return View("Lista", cursos);
        }

        public ActionResult ObtenerCursosPorPrograma(int programa)
        {
            var cursos = repository.ObtenerCursosPorPrograma(programa);
            if (cursos == null || cursos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron cursos";
            }

            return View("Lista", cursos);
        }


    }

}