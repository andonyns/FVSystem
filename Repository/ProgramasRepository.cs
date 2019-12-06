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
    public class ProgramasRepository
    {
        private string connectionString;

        public ProgramasRepository(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Programa> ObtenerProgramas()
        {

            List<Programa> programas = new List<Programa>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT * " +
                                        "FROM Programas";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        programas.Add(new Programa()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            
                        });
                    }
                }
            }
            return programas;
        }

        public List<Programa> ObtenerProgramasPorSede(int sedeId)
        {
            List<Programa> programas = new List<Programa>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT p.*" +
                                        "FROM Programas p " +
                                        "INNER JOIN ProgramasPorSede ps " +
                                        "ON p.IdProgramas = ps.IdProgramas " +
                                        "WHERE ps.IdSede = @Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", sedeId);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var programa = new Programa()
                        {
                            Id = Convert.ToInt32(reader["IdProgramas"]),
                            Nombre = Convert.ToString(reader["NombreDeProgramas"]),
                            
                        };
                        programas.Add(programa);
                    }
                }
            }

            return programas;
        }

        public bool InsertarProgramas(Programa programas)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO Programas(Id,Nombre,FechaInicio,FechaFinal,IdCurso) " +
                                                        "VALUES(@Id,@Nombre,@FechaInicio,@FechaFinal,@IdCurso )", connect))
                {
                    command.Parameters.AddWithValue("@Id", programas.Id);
                    command.Parameters.AddWithValue("@Nombre", programas.Nombre);
                    
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

        public bool ActualizarProgramas(Programa programas)
        {

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "UPDATE Programas " +
                                                        "SET Nombre= @Nombre, FechaInicio= @FechaInicio, FechaFinal= @FechaFinal, IdCurso= @IdCurso " +
                                                        "WHERE Id= @Id", connect))
                {
                    command.Parameters.AddWithValue("@Id", programas.Id);
                    command.Parameters.AddWithValue("@Nombre", programas.Nombre);
                    
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

        public bool BorrarProgramas(string id)
        {

            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "DELETE  " +
                                                        "FROM Programas " +
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

        public List<Programa> ObtenerCursosPorPrograma(int IdProgramas)
        {
            List<Programa> cursos = new List<Programa>();
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (MySqlCommand command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT c.*" +
                                        "FROM Programas p " +
                                        "INNER JOIN Programas p " +
                                        "ON p.IdCursos = cp.IdCursos " +
                                        "WHERE cp.IdPrograma = @Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", IdProgramas);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var Cursos = new Programa()
                        {
                            Id = Convert.ToInt32(reader["IdCursos"]),
                            Nombre = Convert.ToString(reader["NombreDeCursos"]),

                        };
                        cursos.Add(Cursos);
                    }
                }
            }

            return cursos;
        }


    }
}
