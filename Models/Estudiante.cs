using System;

namespace FVSystem.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cedula { get; set; }
        public string Residencia { get; set; }
        public string Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}