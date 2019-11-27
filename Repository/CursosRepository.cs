using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace FVSystem.Repository
{
    public class CursosRepository
    {
        private IConfiguration configuration;
        private string connectionString;

        public CursosRepository(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Curso> ObtenerCursos()
        {
            List<Curso> cursos = new List<Curso>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Cursos";
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cursos.Add(new Curso()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        });
                    }
                }
            }
            return cursos;
        }

       

        public Curso ObtenerCurso(string nombre)
        {
            Curso curso = new Curso();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT * " +
                                        "FROM Cursos " +
                                        "WHERE Id = " + nombre;

                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        curso = new Curso()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        };
                    }
                }
            }
            return curso;
        }

        public List<Curso> ObtenerCursosSede(int sede)
        {
            List<Curso> cursos = new List<Curso>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT c.* " +
                                         "FROM CursosPorSede cs " +
                                        "INNER JOIN Cursos c " +
                                        "ON cs.IdCurso = c.Id "+ 
                                        "WHERE IdSede = @Id";

                    command.Parameters.AddWithValue("@Id", sede);

                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cursos.Add(new Curso()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"])

                        });
                    }
                }
            }
            return cursos;
        }
       

        public bool InsertarCurso(string nombre)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();

                using (var command = new MySqlCommand(
                                                        "INSERT INTO Cursos(Nombre) " +
                                                        "VALUES(@Nombre)", connect))
                {
                    
                    command.Parameters.AddWithValue("@Nombre", nombre);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;

        }

        public bool ActualizarCurso(Curso curso)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = new MySqlCommand(
                                                       @"Update Cursos " +
                                                        "Set Nombre = @Nombre " +
                                                        "Where Id = @Id", connect))
                {
                    command.Parameters.AddWithValue("@Id", curso.Id);
                    command.Parameters.AddWithValue("@Nombre", curso.Nombre);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;
        }

        public bool EliminarCurso(Curso curso)
        {
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = new MySqlCommand(
                                                       @"DELETE FROM Cursos " +
                                                        "WHERE Id = @Id", connect))
                {
                    command.Parameters.AddWithValue("@Id", curso.Id);
                    command.Parameters.AddWithValue("@Nombre", curso.Nombre);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
                connect.Close();
            }

            return true;
        }
    }
}