using FVSystem.Models;
using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace FVSystem.Controllers
{
    public class ModuloController : Controller
    {
        private ModuloRepository repository;
        private CursosRepository cursosRepository;

        public ModuloController(IConfiguration config)
        {
            repository = new ModuloRepository();
            cursosRepository = new CursosRepository(config);
        }

        public ActionResult Index()
        {
            var modulos = repository.ObtenerModulos();
            if (modulos == null || modulos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron modulos";
            }

            return View("Lista", modulos);
        }

        // GET: Modulo
        public ActionResult Agregar()
        {
            var listaCursos = cursosRepository.ObtenerCursos();

            return View(listaCursos);
        }

        public ActionResult Editar(string id)
        {
            var modulo = repository.ObtenerModulo(id);

            return View(modulo);
        }

        public ActionResult Guardar(Modulo modulo)
        {
            repository.InsertarModulos(modulo);

            return Redirect("/Modulo");
        }

        public ActionResult Actualizar(Modulo modulo)
        {
            repository.ActualizarModulo(modulo);

            return Redirect("/Modulo");
        }

        public ActionResult Edit(string id)
        {
            return Redirect("/Modulo");
        }

        public ActionResult Eliminar(string id)
        {
            repository.BorrarModulo(id);

            return Ok();
        }

        public ActionResult ObtenerNota(string moduloId)
        {

            var notas = repository.ObtenerNotasPorModulo(moduloId);
            if (notas == null || notas.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron notas para el modulo: " + moduloId;
            }

            return View("Notas", notas);
        }

        public ActionResult VerModulosCurso(int curso)
        {
            var modulos = repository.ObtenerModulosCurso(curso);
            if (modulos == null || modulos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron modulos";
            }

            return View("Lista", modulos);
        }

    }
}