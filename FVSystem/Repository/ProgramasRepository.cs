using FVSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FVSystem.Repository
{
    public class ProgramasRepository : BaseRepository
    {
        public ProgramasRepository(IConfiguration config, IWebHostEnvironment env) : base(config, env) { }

        public List<Programa> ObtenerProgramas(int sedeId)
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
                                        "ON p.Id = ps.IdPrograma " +
                                        "WHERE ps.IdSede = @Id";
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@Id", sedeId);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var programa = new Programa()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            
                        };
                        programas.Add(programa);
                    }
                }
            }

            return programas;
        }

        public bool InsertarPrograma(Programa programa, int sede)
        {
            int idPrograma = 0;
            using (MySqlConnection connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO Programas(Nombre) " +
                                                        "VALUES(@Nombre)", connect))
                {
                    command.Parameters.AddWithValue("@Nombre", programa.Nombre);
                    
                    try
                    {
                        var result = command.ExecuteNonQuery();
                        idPrograma = Convert.ToInt32(command.LastInsertedId);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                using (MySqlCommand command = new MySqlCommand(
                                                        "INSERT INTO ProgramasPorSede(IdPrograma,IdSede) " +
                                                        "VALUES(@IdPrograma,@IdSede)", connect))
                {
                    if (idPrograma == 0) {
                        return false;
                    }
                    command.Parameters.AddWithValue("@IdPrograma", idPrograma);
                    command.Parameters.AddWithValue("@IdSede", sede);

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

        public List<Programa> ObtenerCursosPorPrograma(int id)
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
                    command.Parameters.AddWithValue("@Id", id);

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
