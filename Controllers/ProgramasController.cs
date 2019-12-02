using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FVSystem.Controllers
{
    public class ProgramasController : Controller
    {
        private ProgramasRepository;
        private CursosRepository cursosRepository;

        public ModuloController(IConfiguration config)
        {
            repository = new ProgramasRepository(config);
            cursosRepository = new CursosRepository(config);
            
            
            public IActionResult Index()
        {
                var programas = repository.ObtenerProgramas();
                if (cursos == null || cursos.Count == 0)
                {
                    ViewBag.ErrorMessage = "No se encontraron programas";
                }

                return View("Lista", programas);

                return View();
        }

            // GET: Modulo
            public ActionResult Agregar()
            {
                var listaProgramas = programasRepository.ObtenerProgramas();

                return View(listaProgramas);
            }

            public ActionResult Editar(string id)
            {
                var programas = repository.ObtenerProgramas(id);

                return View(programas);
            }

            public ActionResult Guardar(Programas programas)
            {
                repository.InsertarProgramas(programas);

                return Redirect("/Programas");
            }

            public ActionResult Actualizar(Programas programas)
            {
                repository.ActualizarProgramas(programas);

                return Redirect("/Programas");
            }
        }
}