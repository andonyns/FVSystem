using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Models
{
    public class ListaCursosConSeleccion
    {
        public List<Curso> ListaCursos { get; set; }
        public int IdCursoSeleccionado { get; set; }
    }
}
