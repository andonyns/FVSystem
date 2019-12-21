using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FVSystem.Models
{
    public class NotaModulo
    {
        public Estudiante Estudiante { get; set; }
        public Modulo Modulo { get; set; }
        public int Nota { get; set; }
    }
}