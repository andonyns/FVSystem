using FVSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace FVSystem.Repository
{
    public class ModuloRepository
    {
        public List<Modulo> ObtenerModulos()
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Modulo> modulos = new List<Modulo>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Modulos";
                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        modulos.Add(new Modulo()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFinal = Convert.ToDateTime(reader["FechaFinal"]),
                            IdCurso = Convert.ToString(reader["IdCurso"])
                        });
                    }
                }
            }
            return modulos;
        }

        public Modulo ObtenerModulo(string id)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            Modulo modulo = new Modulo();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Modulos " +
                                        "WHERE Id=@Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", id);

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        modulo = new Modulo()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            FechaInicio = Convert.ToDateTime(reader["FechaInicio"]),
                            FechaFinal = Convert.ToDateTime(reader["FechaFinal"]),
                            IdCurso = Convert.ToString(reader["IdCurso"])
                        };
                    }
                }
            }

            return modulo;
        }

        public bool InsertarModulos(Modulo modulo)
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
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            var Notas = new List<NotaModulo>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
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

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var estudiante = new Estudiante()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Cedula = Convert.ToString(reader["Cedula"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellidos = Convert.ToString(reader["Apellidos"]),
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

        public List<Modulo> ObtenerModulosCurso(int curso)
        {
            string relativePath = @"Database\FVSystem.db";
            string currentPath;
            string absolutePath;
            string connectionString;

            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            absolutePath = System.IO.Path.Combine(currentPath, relativePath);

            connectionString = string.Format("DataSource={0}", absolutePath);

            List<Modulo> modulos = new List<Modulo>();
            using (SQLiteConnection connect = new SQLiteConnection(connectionString))
            {
                connect.Open();
                using (SQLiteCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT m.* " +
                                         "FROM Cursos c " +
                                        "INNER JOIN Modulos m " +
                                        "ON m.IdCurso = c.Id " +
                                        "WHERE IdCurso = @Id";

                    command.Parameters.AddWithValue("@Id", curso);

                    command.CommandType = CommandType.Text;
                    SQLiteDataReader reader = command.ExecuteReader();
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

    }

}