using FVSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace FVSystem.Repository
{
    public class EstudiantesRepository
    {
        public List<Estudiante> ObtenerEstudiantes()
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Estudiante> sedes = new List<Estudiante>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Estudiantes";
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sedes.Add(new Estudiante()
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
            return sedes;
        }

        public Estudiante ObtenerEstudiante(string id)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            Estudiante estudiante = new Estudiante();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Estudiantes " +
                                        "WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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

        public Estudiante ObtenerEstudiantesPorCurso(string cursoId)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            Estudiante estudiante = new Estudiante();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT E.* " +
                                        "FROM Estudiantes As E " +
                                       " INNER JOIN EstudiantesCurso AS EC" +
                                       "ON E.Id=EC.IdEstudiante" +
                                        "WHERE EC.IdCurso=1";
                    command.Parameters.AddWithValue("@Id", cursoId);
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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

        public bool InsertarEstudiante(Estudiante estudiante)
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

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();


                using (SQLiteCommand command = new SQLiteCommand(
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

            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();


                using (SQLiteCommand command = new SQLiteCommand(
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
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT E.* " +
                                         "FROM Estudiantes As E " +
                                        "INNER JOIN EstudiantesCurso AS EC " +
                                        "ON E.Id=EC.IdEstudiante " +
                                        "WHERE EC.IdCurso=@id";

                    command.Parameters.AddWithValue("@Id", curso);

                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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