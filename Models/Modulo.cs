using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FVSystem.Models
{
    public class Modulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public string IdCurso { get; set; }
    }
}