using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Models
{
    public class ProgramaCurso
    {
        public Curso Curso { get; set; }
        public List<Curso> Programas { get; set; }
        public List<Curso> ListaCursos { get; set; }
        public int IdCursos { get; set; }
    }
}
