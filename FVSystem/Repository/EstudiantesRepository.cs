using FVSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;

namespace FVSystem.Repository
{
    public class EstudiantesRepository : BaseRepository
    {
        public EstudiantesRepository(IConfiguration config, IWebHostEnvironment env) : base(config, env) { }

        public List<Estudiante> ObtenerEstudiantes()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Estudiantes";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiantes.Add(new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Cedula = Convert.ToString(reader["Cedula"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellido = Convert.ToString(reader["Apellido"]),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        });
                    }

                }
            }
            return estudiantes;
        }

        public Estudiante ObtenerEstudiante(string id)
        {
            Estudiante estudiante = new Estudiante();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {
                    command.CommandText = @"SELECT *" +
                                        "FROM Estudiantes " +
                                        "WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiante = new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Cedula = Convert.ToString(reader["Cedula"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellido = Convert.ToString(reader["Apellido"]),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        };
                    }

                }


            }
            return estudiante;
        }

        public List<Estudiante> ObtenerEstudiantesPorCurso(string curso)
        {
            var estudiantes = new List<Estudiante>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT E.* " +
                                        "FROM Estudiantes As E " +
                                       " INNER JOIN EstudiantesCurso AS EC" +
                                       "ON E.Id=EC.IdEstudiante" +
                                        "WHERE EC.IdCurso=1";
                    command.Parameters.AddWithValue("@Id", curso);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiantes.Add(new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Cedula = Convert.ToString(reader["Cedula"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellido = Convert.ToString(reader["Apellido"]),
                            FechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        });
                    }

                }
            }
            return estudiantes;
        }

        public bool InsertarEstudiante(Estudiante estudiante)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (var command = new MySqlCommand(
                                                        "INSERT INTO Estudiantes(Id,Nombre,Apellido,FechaNacimiento,Cedula) " +
                                                        "VALUES(@Id,@Nombre,@Apellido,@FechaNacimiento,@Cedula)", connect))
                {
                    command.Parameters.AddWithValue("@Id", estudiante.Id);
                    command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    command.Parameters.AddWithValue("@Apellido", estudiante.Apellido);
                    command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@Cedula", estudiante.Cedula);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception )
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public bool ActualizarEstudiante(Estudiante estudiante)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (var command = new MySqlCommand(
                                                        "UPDATE Estudiantes " +
                                                        "SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Cedula = @Cedula " +
                                                        "WHERE Id=@Id ", connect))
                {

                    command.Parameters.AddWithValue("@Id", estudiante.Id);
                    command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    command.Parameters.AddWithValue("@Apellido", estudiante.Apellido);
                    command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@Cedula", estudiante.Cedula);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception )
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public bool BorrarEstudiante(string id)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (var command = new MySqlCommand(
                                                        "DELETE FROM Estudiantes " +
                                                        "WHERE Id=@Id ", connect))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception )
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }
        public List<Estudiante> ObtenerEstudiantesCurso(string curso)
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT E.* " +
                                         "FROM Estudiantes As E " +
                                        "INNER JOIN EstudiantesCurso AS EC " +
                                        "ON E.Id=EC.IdEstudiante " +
                                        "WHERE EC.IdCurso=@id";

                    command.Parameters.AddWithValue("@Id", curso);

                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiantes.Add(new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        });
                    }
                }
            }
            return estudiantes;
        }

    }
}