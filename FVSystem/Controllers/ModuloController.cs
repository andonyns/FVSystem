using FVSystem.Models;
using FVSystem.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace FVSystem.Controllers
{
    public class ModuloController : Controller
    {
        private ModuloRepository repository;
        private CursosRepository cursosRepository;

        public ModuloController(IConfiguration config, IWebHostEnvironment env)
        {
            repository = new ModuloRepository(config, env);
            cursosRepository = new CursosRepository(config, env);
        }

        public ActionResult Obtener(int cursoId)
        {
            var modulosCurso = new ModulosCurso()
            {
                Modulos = repository.ObtenerModulos(cursoId),
                Curso = cursosRepository.ObtenerCurso(cursoId)
            };

            if (modulosCurso.Modulos == null || modulosCurso.Modulos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron modulos";
            }

            return View("Lista", modulosCurso);
        }

        // GET: Modulo
        public ActionResult Agregar(int cursoId)
        {
            // Revisar
            var listaCursos = new ListaCursosConSeleccion()
            {
                IdCursoSeleccionado = cursoId,
                ListaCursos = cursosRepository.ObtenerCursos(cursoId)
            };

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

    }
}