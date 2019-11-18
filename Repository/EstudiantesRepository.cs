using FVSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace FVSystem.Repository
{
    public class EstudiantesRepository
    {
        private IConfiguration configuration;
        private string connectionString;

        public EstudiantesRepository(IConfiguration config)
        {
            configuration = config;
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Estudiante> ObtenerEstudiantes()
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM personas " +
                                        "WHERE identificacion is not null " +
                                        "AND fecha_nacimiento is not null";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiantes.Add(new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Cedula = Convert.ToString(reader["identificacion"]),
                            Nombre = Convert.ToString(reader["nombre_completo"]),
                            Apellidos = Convert.ToString(reader["apellidos"]),
                            Residencia = Convert.ToString(reader["residencia"]),
                            Sexo = Convert.ToString(reader["sexo"]),
                            FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                        });
                    }

                }
            }
            return estudiantes;
        }

        public Estudiante ObtenerEstudiante(string id)
        {
            Estudiante estudiante = new Estudiante();
            using (var connect = new MySqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM personas " +
                                        "WHERE id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiante = new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Cedula = Convert.ToString(reader["identificacion"]),
                            Nombre = Convert.ToString(reader["nombre_completo"]),
                            Apellidos = Convert.ToString(reader["apellidos"]),
                            Residencia = Convert.ToString(reader["residencia"]),
                            Sexo = Convert.ToString(reader["sexo"]),
                            FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                        };
                    }

                }
            }
            return estudiante;
        }

        public Estudiante ObtenerEstudiantesPorCurso(string cursoId)
        {
            Estudiante estudiante = new Estudiante();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT E.* " +
                                        "FROM Estudiantes As E " +
                                       " INNER JOIN EstudiantesCurso AS EC" +
                                       "ON E.Id=EC.IdEstudiante" +
                                        "WHERE EC.IdCurso=1";
                    command.Parameters.AddWithValue("@Id", cursoId);
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        estudiante = new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Cedula = Convert.ToString(reader["identificacion"]),
                            Nombre = Convert.ToString(reader["nombre_completo"]),
                            Apellidos = Convert.ToString(reader["apellidos"]),
                            Residencia = Convert.ToString(reader["residencia"]),
                            Sexo = Convert.ToString(reader["sexo"]),
                            FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]),
                        };
                    }

                }
            }
            return estudiante;
        }

        public bool InsertarEstudiante(Estudiante estudiante)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO " +
                                                        "Estudiantes(id, nombre_completo, apellidos, fecha_nacimiento, identificacion, sexo, residencia) " +
                                                        "VALUES(@Id, @Nombre, @Apellido, @FechaNacimiento, @Cedula, @Sexo, @Residencia)", connect))
                {
                    command.Parameters.AddWithValue("@Id", estudiante.Id);
                    command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    command.Parameters.AddWithValue("@Cedula", estudiante.Apellidos);
                    command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@Cedula", estudiante.Cedula);
                    command.Parameters.AddWithValue("@Sexo", estudiante.Sexo);
                    command.Parameters.AddWithValue("@Residencia", estudiante.Residencia);

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

        public bool ActualizarEstudiante(Estudiante estudiante)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "UPDATE Estudiantes " +
                                                        "SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Cedula = @Cedula " +
                                                        "WHERE Id=@Id ", connect))
                {
                    command.Parameters.AddWithValue("@Id", estudiante.Id);
                    command.Parameters.AddWithValue("@Nombre", estudiante.Nombre);
                    command.Parameters.AddWithValue("@Apellido", estudiante.Apellidos);
                    command.Parameters.AddWithValue("@FechaNacimiento", estudiante.FechaNacimiento);
                    command.Parameters.AddWithValue("@Cedula", estudiante.Cedula);
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

        public bool BorrarEstudiante(string id)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "DELETE FROM Estudiantes " +
                                                        "WHERE Id=@Id ", connect))
                {
                    command.Parameters.AddWithValue("@Id", id);

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
        public List<Estudiante> ObtenerEstudiantesCurso(string curso)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Estudiante> estudiantes = new List<Estudiante>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
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