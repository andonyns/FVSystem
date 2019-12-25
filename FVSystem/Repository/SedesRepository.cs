using FVSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FVSystem.Repository
{

    public class SedesRepository : BaseRepository
    {
        public SedesRepository(IConfiguration config, IWebHostEnvironment env) : base(config, env) {}

        public List<Sede> GetSedes()
        {
            List<Sede> sedes = new List<Sede>();
            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();
                using (var command = connect.CreateCommand())
                {

                    command.CommandText = @"SELECT *" +
                                        "FROM Sedes";
                    command.CommandType = CommandType.Text;
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        sedes.Add(new Sede()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Direccion = Convert.ToString(reader["Direccion"])
                        });
                    }
                }
            }
            return sedes;
        }

        public bool InsertarSede(Sede sede)
        {

            using (var connect = new MySqlConnection(connectionString))
            {
                connect.Open();


                using (var command = new MySqlCommand(
                                                        "INSERT INTO Sedes(Id,Nombre,Direccion) " +
                                                        "VALUES(@Id,@Nombre,@Direccion)", connect))
                {
                    command.Parameters.AddWithValue("@Id", sede.Id);
                    command.Parameters.AddWithValue("@Nombre", sede.Nombre);
                    command.Parameters.AddWithValue("@Direccion", sede.Direccion);

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