using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Repository
{
    public class NotasRepository
    {

        private IConfiguration configuration;
        private string connectionString;

        public NotasRepository(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<NotaModulo> DesgloseNotasPorModulo(string moduloId)
        {
            var Notas = new List<NotaModulo>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                command.CommandText = @"SELECT dn.*, nm.Nota " +
                                        "FROM DesgloseDeNotas dn " +
                                         "INNER JOIN NotasModulo nm " +
                                           "ON dn.IdNotas = nm.IdNotas " +
                                                "WHERE IdModulo = @Id";
                command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", moduloId);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var estudiante = new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Cedula = Convert.ToString(reader["Cedula"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellido = Convert.ToString(reader["Apellido"]),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"])
                        };

                        var modulo = new Modulo()
                        {
                            Id = Convert.ToInt32(reader["IdModulo"]),
                            Nombre = Convert.ToString(reader["NombreModulo"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFinal = Convert.ToDateTime(reader["FechaFinal"]),
                            IdCurso = Convert.ToString(reader["IdCurso"])
                        };

                        var nota = new NotaModulo()
                        {
                            Estudiante = estudiante,
                            Modulo = modulo,
                            Nota = Convert.ToInt32(reader["Nota"])
                        };
                        Notas.Add(nota);
                    }
                }
            }

            return Notas;
        }
    }
}
