using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FVSystem.Models;
using FVSystem.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FVSystem.Controllers
{
    public class ProgramasController : Controller
    {
        private ProgramasRepository programasRepository;


        public ProgramasController(IConfiguration config)
        {
            programasRepository = new ProgramasRepository(config);

        }

        public IActionResult Index()
        {
            var programas = programasRepository.ObtenerProgramas();
            if (programas == null || programas.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron programas";
            }

            return View("Lista", programas);
        }

        // GET: Programas
        public ActionResult Agregar()
        {
            var listaProgramas = programasRepository.ObtenerProgramas();

            return View(listaProgramas);
        }

        public ActionResult Editar(string id)
        {
            //var programas = programasRepository.ObtenerProgramas(id);

            return View();
        }

        public ActionResult Guardar(Programa programa)
        {
            //programasRepository.InsertarProgramas(programa);

            return Redirect("/Programas");
        }

        public ActionResult Actualizar(Programa programa)
        {
            //programasRepository.ActualizarProgramas(programas);

            return Redirect("/Programas");
        }

        public ActionResult Edit(string id)
        {
            return Redirect("/Programas");
        }

        public ActionResult Eliminar(string id)
        {
            programasRepository.BorrarProgramas(id);

            return Ok();
        }

        public ActionResult ProgramasPorSede(int sede)
        {
            var programas = programasRepository.ObtenerProgramasPorSede(sede);
            if (programas == null || programas.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron cursos";
            }

            return View("Lista", programas);
        }

        public ActionResult CursosPorPrograma(int programa)
        {
            var cursos = programasRepository.ObtenerCursosPorPrograma(programa);
            if (cursos == null || cursos.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron cursos";
            }

            return View("Lista", cursos);
        }


    }
}