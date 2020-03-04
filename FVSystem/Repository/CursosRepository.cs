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
    public class CursosRepository : BaseRepository
    {
        public CursosRepository(IConfiguration config, IWebHostEnvironment env) : base(config, env) { }

        public List<Curso> ObtenerCursos(int programa)
        {
            List<Curso> cursos = new List<Curso>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT c.* " +
                                         "FROM CursosPorPrograma cp " +
                                        "INNER JOIN Cursos c " +
                                        "ON cp.IdCursos = c.Id " +
                                        "WHERE IdProgramas = @Id";

                    command.Parameters.AddWithValue("@Id", programa);

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



        public Curso ObtenerCurso(int id)
        {
            Curso curso = new Curso();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT * " +
                                        "FROM Cursos " +
                                        "WHERE Id = " + id;

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

        public bool InsertarCurso(string nombre, int programa)
        {
            int idCurso = 0;
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
                        idCurso = Convert.ToInt32(command.LastInsertedId);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO CursosPorPrograma(IdCursos, IdProgramas) " +
                                                        "VALUES(@IdCurso,@IdPrograma)", connect))
                {
                    if (idCurso == 0)
                    {
                        return false;
                    }
                    command.Parameters.AddWithValue("@IdCurso", idCurso);
                    command.Parameters.AddWithValue("@IdPrograma", programa);

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
                    catch (Exception)
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
                    catch (Exception)
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