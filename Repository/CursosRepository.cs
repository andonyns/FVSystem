using FVSystem.Models;
using Microsoft.Extensions.Configuration;
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
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Curso> cursos = new List<Curso>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Cursos";
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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

        public Curso ObtenerCurso(string id)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            Curso curso = new Curso();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT * " +
                                        "FROM Cursos " +
                                        "WHERE Id = " + id;

                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Curso> cursos = new List<Curso>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT c.* " +
                                         "FROM CursosPorSede cs " +
                                        "INNER JOIN Cursos c " +
                                        "ON cs.IdCurso = c.Id "+ 
                                        "WHERE IdSede = @Id";

                    command.Parameters.AddWithValue("@Id", sede);

                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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
       

        public bool InsertarCurso(Curso curso)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();

                using (SQLiteCommand command = new SQLiteCommand(
                                                        "INSERT INTO Cursos(Id,Nombre) " +
                                                        "VALUES(@Id,@Nombre)", connect))
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

        public bool ActualizarCurso(Curso curso)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = new SQLiteCommand(
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
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = new SQLiteCommand(
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