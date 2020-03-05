using FVSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace FVSystem.Repository
{
    public class ModuloRepository : BaseRepository
    {
        public ModuloRepository(IConfiguration config, IWebHostEnvironment env) : base(config, env) { }

        public List<Modulo> ObtenerModulos(int curso)
        {
            List<Modulo> modulos = new List<Modulo>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT m.* " +
                                         "FROM Cursos c " +
                                        "INNER JOIN Modulos m " +
                                        "ON m.IdCurso = c.Id " +
                                        "WHERE IdCurso = @Id";

                    command.Parameters.AddWithValue("@Id", curso);

                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        modulos.Add(new Modulo()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        });
                    }
                }
            }
            return modulos;
        }

        public Modulo ObtenerModulo(string id)
        {    
            Modulo modulo = new Modulo();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Modulos " +
                                        "WHERE Id=@Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", id);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        modulo = new Modulo()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFinal = Convert.ToDateTime(reader["FechaFinal"]),
                            IdCurso = Convert.ToString(reader["IdCurso"]),
                           
                        };
                    }
                }
            }

            return modulo;
        }

        public bool InsertarModulos(Modulo modulo)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO Modulos(Id,Nombre,FechaInicio,FechaFinal,IdCurso) " +
                                                        "VALUES(@Id,@Nombre,@FechaInicio,@FechaFinal,@IdCurso )", connect))
                {
                    command.Parameters.AddWithValue("@Id", modulo.Id);
                    command.Parameters.AddWithValue("@Nombre", modulo.Nombre);
                    command.Parameters.AddWithValue("@FechaInicio", modulo.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFinal", modulo.FechaFinal);
                    command.Parameters.AddWithValue("@IdCurso", modulo.IdCurso);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public bool ActualizarModulo(Modulo modulo)
        {

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "UPDATE Modulos " +
                                                        "SET Nombre= @Nombre, FechaInicio= @FechaInicio, FechaFinal= @FechaFinal, IdCurso= @IdCurso " +
                                                        "WHERE Id= @Id", connect))
                {
                    command.Parameters.AddWithValue("@Id", modulo.Id);
                    command.Parameters.AddWithValue("@Nombre", modulo.Nombre);
                    command.Parameters.AddWithValue("@FechaInicio", modulo.FechaInicio);
                    command.Parameters.AddWithValue("@FechaFinal", modulo.FechaFinal);
                    command.Parameters.AddWithValue("@IdCurso", modulo.IdCurso);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public bool BorrarModulo(string id)
        {

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "DELETE  " +
                                                        "FROM Modulos " +
                                                        "WHERE Id= @Id", connect))
                {
                    command.Parameters.AddWithValue("@Id", id);
                   
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public List<NotaModulo> ObtenerNotasPorModulo(string moduloId)
        {
            var Notas = new List<NotaModulo>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT e.*, m.Id as IdModulo, m.Nombre as NombreModulo, m.FechaInicio, m.FechaFinal, m.IdCurso, nm.Nota " +
                                            "From Estudiantes e "+
                                            "INNER JOIN NotasModulos nm "+
                                            "INNER JOIN Modulos m "+
                                            "Where e.Id = nm.IdEstudiante "+
                                            "AND nm.IdModulo = m.Id "+
                                            "AND nm.IdModulo = @Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", moduloId);

                    MySqlDataReader reader = command.ExecuteReader();
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