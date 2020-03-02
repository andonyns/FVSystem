using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FVSystem.Models;
using FVSystem.Repository;
using FVSystem.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FVSystem.Controllers
{
    public class ProgramasController : Controller
    {
        private readonly ProgramasRepository programasRepository;

        public ProgramasController(IConfiguration config, IWebHostEnvironment env)
        {
            programasRepository = new ProgramasRepository(config, env);

        }

        public ActionResult Obtener(int sede)
        {
            var programas = programasRepository.ObtenerProgramas(sede);
            if (programas == null || programas.Count == 0)
            {
                ViewBag.ErrorMessage = "No se encontraron programas";
            }

            return View("Lista", new ProgramasSede { Programas = programas, Sede = sede });
        }


        public ActionResult Agregar(int sede)
        {
            return View(sede);
        }

        public ActionResult Editar(string id)
        {
            //var programas = programasRepository.ObtenerProgramas(id);

            return View();
        }

        public ActionResult Guardar(Programa programa, int sede)
        {
            programasRepository.InsertarPrograma(programa, sede);

            return Redirect("/Programas/Obtener?sede="+sede);
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