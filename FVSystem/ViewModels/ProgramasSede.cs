using FVSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.ViewModels
{
    public class ProgramasSede
    {
        public int Sede { get; set; }
        public List<Programa> Programas {get;set;}
    }
}
